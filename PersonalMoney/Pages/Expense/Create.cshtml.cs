using Humanizer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Utilities;
using PersonalMoney.Models;
namespace PersonalMoney.Pages.Expense
{
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

        public IActionResult OnGet()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Categories = _context.Categories.Where(c => c.IsIncome == false && c.UserId == user.Id.ToString()).ToList();
            return Page();
        }
        public IActionResult OnPost()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            pay(-Transaction.Amount);
            Transaction.UserId = user.Id;
            Transaction.CategoryId = SelectedCategoryId;
            Transaction.WalletId = int.Parse(WalletId);
            Transaction.DateOfTransaction = DateTime.Now;
            _context.Transactions.Add(Transaction);
            _context.SaveChanges();
            return Redirect("/expense");
        }
        public void pay(decimal amount) {
            var wallet = _context.Wallets.FirstOrDefault(x => x.Id == int.Parse(WalletId));
            if(wallet != null)
            {
                wallet.Balance += amount;
                _context.Wallets.Update(wallet);
                _context.SaveChanges();
            }
        }
    }
}
