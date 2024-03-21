using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
using System.Numerics;

namespace PersonalMoney.Pages.Wallets
{
    public class EditModel : PageModel
    {
        private readonly PersonalMoneyContext _context;

        [BindProperty]
        public Wallet Wallet { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public EditModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            var wallet = _context.Wallets.FirstOrDefault(d => d.Id == id);

            if (wallet != null)
            {
                Wallet = wallet;
            }
            else
            {
                StatusMessage = "Wallets not exits!";
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var wallet = _context.Wallets.FirstOrDefault(d => d.Id == Wallet.Id);

            if (wallet != null)
            {
                wallet.Name = Wallet.Name;
                wallet.Balance = Wallet.Balance;
                _context.Wallets.Update(wallet);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a Wallet successfully!";
                return Redirect("/Index");
            }

            StatusMessage = "Wallet not exists!";
            return Page();
        }
    }
}
