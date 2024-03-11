using Humanizer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Bcpg;
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
            Categories = _context.Categories.Where(c=> c.IsIncome == false && c.UserId == user.Id.ToString()).ToList();
            return Page();
        }
        public IActionResult OnPost()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Transaction.UserId = user.Id;
            Transaction.CategoryId = SelectedCategoryId;
            Transaction.WalletId = int.Parse(WalletId);
            return Page();
        }
    }
}
