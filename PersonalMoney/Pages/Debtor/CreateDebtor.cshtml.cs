using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace PersonalMoney.Pagesg
{
    [Authorize]
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

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }
            ViewData["userId"] = user.Id.ToString();
            debtor = new Debtor
            {
                User = user
            };
            return Page();
        }

        public IActionResult OnPost()
        {

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            debtor.DateCreate = DateTime.Now;
            debtor.DateUpdate = DateTime.Now;
            debtor.TotalMoney = 0;

            _context.Debtors.Add(debtor);
            _context.SaveChanges();
            StatusMessage = "Created a debtor successfully!";

            return RedirectToPage("/debtor/index");
        }
    }
}