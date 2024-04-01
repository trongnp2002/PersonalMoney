using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalMoney.dto.BudgetDto;
using PersonalMoney.dto.CategoryDto;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.BudgetPage
{
    [Authorize]
    public class Index : BasePageModel
    {
        private readonly ILogger<Index> _logger;

        public Index(ILogger<Index> logger, PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {


            return Page();
        }


        public async Task<IActionResult> OnGetData(string? month, string? year)
        {
           
            var categories = new List<ProcessBudget>();
            var user = await _userManager.GetUserAsync(User);
            var cates = await _dbContext.Categories.Where(c => c.UserId.Equals(user.Id) && c.IsIncome.Equals(false)).ToListAsync();
            var budget = await _dbContext.Budgets.FirstOrDefaultAsync(b => b.UserId.Equals(user.Id));
            decimal budExpense = budget == null ? 0 : budget.MonthlySpending == null ? 0 : (decimal)budget.MonthlySpending;
            List<ProcessBudget> processBudgets = new List<ProcessBudget>();
            BudgetResponse budgetResponse = new BudgetResponse().AsBuilder()
                .WithTotalExpense(0).WithBudgetExpense(budExpense).Build();
            DateTime now = DateTime.Now;
            DateTime lastDayOfMonth = now;
            if(String.IsNullOrEmpty(month) || String.IsNullOrEmpty(year))
            {
                month = string.Empty;
                year = string.Empty;
            }
            if (!String.IsNullOrEmpty(month) && !String.IsNullOrEmpty(year))
            {
                 lastDayOfMonth = new DateTime(int.Parse(year), int.Parse(month), 1).AddMonths(1).AddDays(-1);
               
            }
            bool check = lastDayOfMonth.CompareTo(now) > 0;
            foreach (var category in cates)
            {
                decimal total = await _dbContext.Transactions.Include(t => t.Category).Where(t => t.CategoryId
                    .Equals(category.Id) && t.DateOfTransaction.Month.Equals(check ? now.Month : lastDayOfMonth.Month)
                    && t.DateOfTransaction.Year.Equals(check ? now.Year : lastDayOfMonth.Year) && category.IsIncome.Equals(false))
                    .SumAsync(t => t.Amount);
                int count = await _dbContext.Transactions.Include(t => t.Category).Where(t => t.CategoryId
                    .Equals(category.Id) && t.DateOfTransaction.Month.Equals(check ? now.Month : lastDayOfMonth.Month)
                    && t.DateOfTransaction.Year.Equals(check? now.Year : lastDayOfMonth.Year) && category.IsIncome.Equals(false))
                    .CountAsync();

                processBudgets.Add(new ProcessBudget().AsBuilder()
                    .WithBudget(category.Budget).WithIsIncome(category.IsIncome)
                    .WithLastUpdate(check ? now: lastDayOfMonth).WithName(category.Name)
                    .WithTotal(total).WithCount(count).WithId(category.Id).Build());
                if (!category.IsIncome)
                {
                    budgetResponse.TotalExpense = budgetResponse.TotalExpense + total;
                }
            }
            budgetResponse.processBudgets = processBudgets;

            return ResponseContent<BudgetResponse>(budgetResponse);
        }

        public async Task<IActionResult> OnPostAdjust([FromBody] BudgetDTO.Adjust adjust)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                throw new ApplicationException(string.Join(", ", errorMessages));
            }
            if (adjust.Earn.CompareTo(0)<=0 || adjust.Spend.CompareTo(0)<=0)
            {
                throw new ApplicationException("Earning and Spending money must be greater than 0");
            }
            var user = await _userManager.GetUserAsync(User);
            try
            {
                var budget = await _dbContext.Budgets.FirstOrDefaultAsync(b => b.UserId.Equals(user.Id));
                if (budget == null)
                {
                    var newBudget = new PersonalMoney.Models.Budget().AsBuilder()
                            .WithMonthlyEarning(adjust.Earn).WithMonthlySpending(adjust.Spend).WithUserId(user.Id)
                            .WithAnnuallySpending(adjust.Spend * 12).WithMonthlySaving(adjust.Earn - adjust.Spend)
                            .Build();
                    _dbContext.Budgets.Add(newBudget);
                    await _dbContext.SaveChangesAsync();

                    return ResponseCreated();

                }
                else
                {
                    budget.MonthlyEarning = adjust.Earn;
                    budget.MonthlySpending = adjust.Spend;
                    budget.AnnuallySpending = adjust.Spend * 12;
                    budget.MonthlySaving = adjust.Earn - adjust.Spend;
                    _dbContext.Budgets.Update(budget);
                    await _dbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return ResponseOK();
        }
        public async Task<IActionResult> OnGetAdjust()
        {
            var user = await _userManager.GetUserAsync(User);
            var budget = _dbContext.Budgets.FirstOrDefault(u => u.UserId.Equals(user.Id));
            var budgetDto = new BudgetDTO();
            if (budget != null)
            {
                budgetDto.AnnuallySpending = budget.AnnuallySpending;
                budgetDto.MonthlyEarning = budget.MonthlyEarning;
                budgetDto.MonthlySaving = budget.MonthlySaving;
                budgetDto.MonthlySpending = budget.MonthlySpending;
            }

            return ResponseContent<BudgetDTO>(budgetDto);
        }

        public async Task<IActionResult> OnPostDistribute([FromBody] DistributeRequest distributeRequest)
        {
            if (distributeRequest == null)
            {
                throw new ApplicationException("Data cannot be null or empty!");
            }
            if (distributeRequest.DistributeList.Count > 0)
            {
                List<Category> categories = new List<Category>();
                foreach (var cat in distributeRequest.DistributeList)
                {
                    var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id.Equals(cat.Id));
                    if (category != null)
                    {
                        category.Budget = cat.Budget;
                        categories.Add(category);
                    }

                }
                _dbContext.Categories.UpdateRange(categories);
                await _dbContext.SaveChangesAsync();
            }

            return ResponseOK();
        }
    }
}