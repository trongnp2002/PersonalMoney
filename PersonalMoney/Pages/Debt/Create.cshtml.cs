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
            _context.DebtDetails.Add(debt);
            _context.SaveChanges();
            return Redirect($"/debtor/details/debt/{debt.DebtorId}");
        }
    }
}