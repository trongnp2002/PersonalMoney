using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using System;
using System.Threading.Tasks;

namespace PersonalMoney.Pagegi
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public DebtDetail debt { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public EditModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet(int id)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            var de = _context.DebtDetails.Include(d => d.Wallet).FirstOrDefault(mo => mo.Id == id);
            var debtor = _context.Debtors.Include(d => d.DebtDetails).FirstOrDefault(d => d.Id == de.DebtorId);

            if (de != null)
            {

                if (de.Classify)
                {
                    de.Wallet.Balance += de.MoneyDebt;
                    debtor.TotalMoney -= de.MoneyDebt;

                }
                else if (!de.Classify)
                {
                    de.Wallet.Balance -= de.MoneyDebt;
                    debtor.TotalMoney += de.MoneyDebt;
                }

                debt = de;
            }
            else
            {
                StatusMessage = "Debt not exits!";
            }
            _context.SaveChanges();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var debtUpdate = _context.DebtDetails.Include(d => d.Wallet).FirstOrDefault(mo => mo.Id == debt.Id);
            var debtor = _context.Debtors.Include(d => d.DebtDetails).FirstOrDefault(d => d.Id == debtUpdate.DebtorId);
            if (debtUpdate != null)
            {
                if (debt.Classify)
                {

                    if (debtUpdate.Wallet.Balance > debt.MoneyDebt)
                    {
                        debtUpdate.Wallet.Balance -= debt.MoneyDebt;
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
                    debtUpdate.Wallet.Balance += debt.MoneyDebt;
                    debtor.TotalMoney -= debt.MoneyDebt;
                }

                debtUpdate.Note = debt.Note;
                debtUpdate.Classify = debt.Classify;
                debtUpdate.MoneyDebt = debt.MoneyDebt;
                debtUpdate.DateDebt = debt.DateDebt;

                _context.DebtDetails.Update(debtUpdate);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a debt successfully!";
                return Redirect($"/debtor/details/debt/{debtUpdate.DebtorId}");

            }

            StatusMessage = "Update debt fail!";
            return Page();
        }
    }
}

