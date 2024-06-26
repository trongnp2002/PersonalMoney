using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Wallets
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Wallet WalletDelete { get; set; }
        public DeleteModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var wallet = _context.Wallets.FirstOrDefault(mo => mo.Id == id);

            if (wallet != null)
            {
                WalletDelete = wallet;
            }
            else
            {
                StatusMessage = "Wallet not exists!";
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (id != null)
            {
                var p = _context.Wallets.FirstOrDefault(mo => mo.Id == id);
                if (p != null)
                {
                    _context.Wallets.Remove(p);
                    _context.SaveChanges();
                    StatusMessage = "Deleted a Wallet successfully!";
                    return Redirect("/");
                }
            }
            return NotFound();
        }
    }
}
