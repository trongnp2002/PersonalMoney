using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Income
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public int SelectedCategoryId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Transaction Income { get; set; }
        [BindProperty(Name = "WalletId")]
        public string WalletId { get; set; }

        public EditModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            Categories = new List<Category>();
        }

        public void OnGet(int id)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Categories = _context.Categories.Where(c => c.IsIncome == true && c.UserId == user.Id.ToString()).ToList();
            var income = _context.Transactions.FirstOrDefault(d => d.Id == id);

            if (income != null)
            {
                Income = income;
            }
            else
            {
                StatusMessage = "Transaction not exits!";
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var income = _context.Transactions.FirstOrDefault(d => d.Id == Income.Id);

            if (income != null)
            {
                if (income.Amount != Income.Amount)
                {
                    pay(Income.Amount - income.Amount);
                }
                income.Title = Income.Title;
                income.Amount = Income.Amount;
                income.CategoryId = SelectedCategoryId;
                _context.Transactions.Update(income);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a Expense successfully!";
                return Redirect("/income");
            }
            StatusMessage = "Income not exists!";
            return Page();
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
