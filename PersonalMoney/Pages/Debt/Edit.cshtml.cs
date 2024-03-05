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

            var de = _context.DebtDetails.FirstOrDefault(d => d.Id == id);

            if (de != null)
            {
                debt = de;
            }
            else
            {
                StatusMessage = "Debt not exits!";
            }

        }


        public async Task<IActionResult> OnPostAsync(bool confirmDelete = false)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (!confirmDelete)
            {
                return RedirectToPage("/debtor/details/debt/" + debt.DebtorId);
            }
            var debtUpdate = _context.DebtDetails.FirstOrDefault(d => d.Id == debt.Id);

            if (debtUpdate != null)
            {
                debtUpdate.Note = debt.Note;
                debtUpdate.Classify = debt.Classify;
                debtUpdate.MoneyDebt = debt.MoneyDebt;
                debtUpdate.DateDebt = debt.DateDebt;

                _context.DebtDetails.Update(debtUpdate);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a debt successfully!";
                return RedirectToPage("/debtor/details/debt/" + debtUpdate.DebtorId);
            }

            StatusMessage = "Update debt fail!";
            return Page();
        }
    }
}
