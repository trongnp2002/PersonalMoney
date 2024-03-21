using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
namespace PersonalMoney.Pages.Expense
{
    public class indexModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public List<Transaction> Transactions { get; set; }

        public indexModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            Transactions = new List<Transaction>();
        }

        public IActionResult OnGet()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Transactions = _context.Transactions.Include(x => x.Wallet).Include(x => x.Category).Where(t=> t.Category.IsIncome== false && t.UserId == user.Id.ToString()).ToList();
            return Page();
        }

    }
}
