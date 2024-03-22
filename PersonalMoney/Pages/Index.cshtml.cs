using Microsoft.AspNetCore.Authorization;
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
        public IndexModel( PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

        public IActionResult OnGet()
        {
            ListWallets = _dbContext.Wallets.ToList();
            getTotalIncomeAndExpenseInMonth();
            getMonthlyTotals();
            getTotalMoney();
            getTotalSaving();
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

        public void getMonthlyTotals()
        {
            var monthlyIncome = _dbContext.Transactions
                .Include(t=> t.Category)
                .Where(t=>t.Category.IsIncome==true)
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
                .Where(t => t.Category.IsIncome == false)
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

        public void getTotalIncomeAndExpenseInMonth()
        {
            listTransactionInMonth = new List<Transaction>();
            var currentMonth = DateTime.Now.Month;
            listTransactionInMonth = _dbContext.Transactions.Include(c=> c.Category).Where(t=>t.DateOfTransaction.Month == currentMonth).ToList();
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
        public void getTotalSaving()
        {
            totalSavingInMonth =  totalIncomeInMonth - totalExpenseInMonth;
        }
        public void getTotalMoney()
        {
            totalMoney = 0;
            foreach(var item in ListWallets)
            {
                totalMoney += item.Balance;
            }
        }
    }
}
