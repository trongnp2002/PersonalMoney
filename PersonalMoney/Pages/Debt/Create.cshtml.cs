using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using System;
using System.Threading.Tasks;

namespace PersonalMoney.PageDebt
{
    public class CreateModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public DebtDetail debt { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            ViewData["debtorId"] = id.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var wallet = _context.Wallets.FirstOrDefault(d => d.Id == debt.WalletId);
            var debtor = _context.Debtors.Include(d => d.DebtDetails).FirstOrDefault(d => d.Id == debt.DebtorId);
            if (wallet != null && wallet.IsActive == true)
            {
                if (debt.Classify)
                {

                    if (wallet.Balance > debt.MoneyDebt)
                    {
                        wallet.Balance -= debt.MoneyDebt;
                        debtor.TotalMoney += debt.MoneyDebt;
                    }
                    else
                    {
                        StatusMessage = "The wallet you selected has insuffucient balance!";
                        return Redirect($"/debtor/details/debt/{debt.DebtorId}");
                    }

                }
                else if (!debt.Classify)
                {
                    wallet.Balance += debt.MoneyDebt;
                    debtor.TotalMoney -= debt.MoneyDebt;
                }
            }
            else
            {
                StatusMessage = "The wallet you selected is invalid!";
                return Redirect($"/debtor/details/debt/{debt.DebtorId}");
            }
            _context.DebtDetails.Add(debt);
            _context.SaveChanges();
            StatusMessage = "Create new debt successfully!";
            return Redirect($"/debtor/details/debt/{debt.DebtorId}");
        }
    }
}