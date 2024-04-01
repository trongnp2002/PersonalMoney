using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;
using System.Diagnostics;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public IndexModel(PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }

        [BindProperty]
        public List<Wallet> ListWallets { get; set; }
        [BindProperty]
        public decimal totalIncomeInMonth { get; set; }
        [BindProperty]
        public decimal totalExpenseInMonth { get; set; }
        [BindProperty]
        public decimal totalSavingInMonth { get; set; }
        [BindProperty]
        public decimal totalMoney { get; set; }
        public List<Transaction> listTransactionInMonth { get; set; }
        [BindProperty]
        public decimal[] monthlyIncomeTotals { get; set; }
        [BindProperty]
        public decimal[] monthlyExpenseTotals { get; set; }

        public IActionResult OnGet()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            ListWallets = _dbContext.Wallets
                .Where(u=>u.UserId.Equals(user.Id)).ToList();
            getTotalIncomeAndExpenseInMonth(user.Id);
            getMonthlyTotals(user.Id);
            getTotalMoney(user.Id);
            getTotalSaving(user.Id);
            return Page();
        }

        public IActionResult OnPost(int id)
        { 
            var wallet = _dbContext.Wallets.Find(id);
            if (wallet != null)
            {
                _dbContext.Wallets.Remove(wallet);
                _dbContext.SaveChanges();
            }
            return Page();
        }

        public void getMonthlyTotals(string id)
        {
            var monthlyIncome = _dbContext.Transactions
                .Include(t=> t.Category)
                .Where(t=>t.Category.IsIncome==true && t.UserId.Equals(id))
                .GroupBy(t => new { t.DateOfTransaction.Year, t.DateOfTransaction.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderBy(mt => mt.Year)
                .ThenBy(mt => mt.Month)
                .ToList();
            decimal[] monthlyIncomeToString = new decimal[12];
            foreach (var item in monthlyIncome)
            {
                int monthIndex = item.Month - 1;
                monthlyIncomeToString[monthIndex] = item.TotalAmount;
            }
            var monthlyExpense = _dbContext.Transactions
                .Include(t => t.Category)
                .Where(t => t.Category.IsIncome == false && t.UserId.Equals(id))
                .GroupBy(t => new { t.DateOfTransaction.Year, t.DateOfTransaction.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderBy(mt => mt.Year)
                .ThenBy(mt => mt.Month)
                .ToList();
            decimal[] monthlyExpenseToString = new decimal[12];
            foreach (var item in monthlyExpense)
            {
                int monthIndex = item.Month - 1;
                monthlyExpenseToString[monthIndex] = item.TotalAmount;
            }
            monthlyIncomeTotals = monthlyIncomeToString;
            monthlyExpenseTotals = monthlyExpenseToString;
            ViewData["IncomeInMonth"] = monthlyIncomeTotals;
            ViewData["ExpenseInMonth"] = monthlyExpenseTotals;
        }

        public void getTotalIncomeAndExpenseInMonth(string id)
        {
            listTransactionInMonth = new List<Transaction>();
            var currentMonth = DateTime.Now.Month;
            listTransactionInMonth = _dbContext.Transactions.Include(c=> c.Category)
                .Where(t=>t.DateOfTransaction.Month == currentMonth && t.UserId.Equals(id)).ToList();
            foreach (var transaction in listTransactionInMonth)
            {
                if(transaction.Category.IsIncome == true)
                {
                    totalIncomeInMonth += transaction.Amount;
                }
                else
                {
                    totalExpenseInMonth += transaction.Amount;
                }
            }
        }
        public void getTotalSaving(string id)
        {
            totalSavingInMonth =  totalIncomeInMonth - totalExpenseInMonth;
        }
        public void getTotalMoney(string id)
        {
            totalMoney = 0;
            foreach(var item in ListWallets)
            {
                totalMoney += item.Balance;
            }
        }
    }
}
