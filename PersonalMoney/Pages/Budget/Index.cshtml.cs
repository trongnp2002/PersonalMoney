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
using PersonalMoney.dto.CategoryDto;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Budget
{
    [Authorize]
    public class Index : BasePageModel
    {
        private readonly ILogger<Index> _logger;

        public Index(ILogger<Index> logger, PersonalMoneyContext dbContext ,UserManager<User> userManager) : base(dbContext, userManager)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
        

            return Page();
        }


        public async Task<IActionResult> OnGetData()
        {
            var categories = new List<ProcessBudget>();
            var user = await _userManager.GetUserAsync(User);
            var cates = await _dbContext.Categories.Where(c => c.UserId.Equals(user.Id)).ToListAsync();
            var budget = await _dbContext.Budgets.FirstOrDefaultAsync(b => b.UserId.Equals(user.Id));
            decimal budExpense = budget == null ? 0 : budget.MonthlySpending == null ? 0 : (decimal)budget.MonthlySpending;
            List<ProcessBudget> processBudgets = new List<ProcessBudget>();
            BudgetResponse budgetResponse = new BudgetResponse().AsBuilder()
                .WithTotalExpense(0).WithBudgetExpense(budExpense).Build();
            foreach (var category in cates)
            {
                decimal total = await _dbContext.Transactions.Include(t => t.Category).Where(t => t.CategoryId
                    .Equals(category.Id) && t.DateOfTransaction.Month.Equals(category.LastUpdate.Value.Month)
                    && t.DateOfTransaction.Year.Equals(category.LastUpdate.Value.Year))
                    .SumAsync(t => t.Amount);
                int count = await _dbContext.Transactions.Include(t => t.Category).Where(t => t.CategoryId
                    .Equals(category.Id) && t.DateOfTransaction.Month.Equals(category.LastUpdate.Value.Month)
                    && t.DateOfTransaction.Year.Equals(category.LastUpdate.Value.Year))
                    .CountAsync();
                processBudgets.Add(new ProcessBudget().AsBuilder()
                    .WithBudget(category.Budget).WithIsIncome(category.IsIncome)
                    .WithLastUpdate(category.LastUpdate).WithName(category.Name)
                    .WithTotal(total).WithCount(count).Build());
                if (!category.IsIncome)
                {
                    budgetResponse.TotalExpense = budgetResponse.TotalExpense + total;
                }
            }
            budgetResponse.processBudgets= processBudgets;

            return ResponseContent<BudgetResponse>(budgetResponse);
        }
    }
}