using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Income
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Transaction Transaction { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public int SelectedCategoryId { get; set; }
        [BindProperty(Name = "WalletId")]
        public string WalletId { get; set; }
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            Categories = new List<Category>();

        }
        public IActionResult OnGet(string? error)
        {
            if (!String.IsNullOrEmpty(error))
            {
                TempData["StatusMessage"] = error;
            }
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Categories = _context.Categories.Where(c => c.IsIncome == true && c.UserId == user.Id.ToString()).ToList();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (String.IsNullOrEmpty(WalletId) || WalletId.Trim().Equals("0"))
            {
                return Redirect("/income/create/?error=Wallet id cannot be empty or null!");
            }
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            if (!ModelState.IsValid)
            {
                user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                Categories = _context.Categories.Where(c => c.IsIncome == true && c.UserId == user.Id.ToString()).ToList();
                return Page();
            }
            pay(Transaction.Amount);
            Transaction.UserId = user.Id;
            Transaction.CategoryId = SelectedCategoryId;
            Transaction.WalletId = int.Parse(WalletId);
            _context.Transactions.Add(Transaction);
            _context.SaveChanges();
            return Redirect("/income");
        }
        public void pay(decimal amount)
        {
            var wallet = _context.Wallets.FirstOrDefault(x => x.Id == int.Parse(WalletId));
            if (wallet != null)
            {
                wallet.Balance += amount;
                _context.Wallets.Update(wallet);
                _context.SaveChanges();
            }
        }
    }
}
