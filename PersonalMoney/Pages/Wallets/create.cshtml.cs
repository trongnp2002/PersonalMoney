using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Utilities;
using PersonalMoney.Models;
using System;
using System.Runtime.InteropServices;

namespace PersonalMoney.Pages.Wallets
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Wallet Wallet { get; set; }
        public readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;


        public CreateModel(PersonalMoneyContext context, UserManager<User> user)
        {
            _context= context;
            _userManager= user;
           
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            Wallet.UserId = user.Id;
            Wallet.IsActive = true;
            _context.Wallets.Add(Wallet);
            await _context.SaveChangesAsync();
            return Page();  
        }
    }
}
