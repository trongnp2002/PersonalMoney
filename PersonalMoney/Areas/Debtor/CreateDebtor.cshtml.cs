using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using System;
using System.Threading.Tasks;

namespace PersonalMoney.Pagess
{
    public class CreateDebtorModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        public CreateDebtorModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Debtor debtor { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            debtor.DateCreate = DateTime.Now;
            debtor.DateUpdate = DateTime.Now;
            debtor.UserId = user.Id.ToString();

            _context.Debtors.Add(debtor);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}