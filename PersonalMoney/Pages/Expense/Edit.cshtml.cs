using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Expense
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
        public Transaction Expense { get; set; }
        [BindProperty(Name = "WalletId")]
        public string WalletId { get; set; }


        public EditModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            Categories = new List<Category>();
            _userManager = userManager;
        }

        public void OnGet(int id)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Categories = _context.Categories.Where(c => c.IsIncome == false && c.UserId == user.Id.ToString()).ToList();
            var expense = _context.Transactions.FirstOrDefault(d => d.Id == id);

            if (expense != null)
            {
                Expense = expense;
            }
            else
            {
                StatusMessage = "Transaction not exits!";
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var expense = _context.Transactions.FirstOrDefault(d => d.Id == Expense.Id);

            if (expense != null)
            {
                if(expense.Amount != Expense.Amount)
                {
                    pay(expense.Amount - Expense.Amount);
                }
                expense.Title = Expense.Title;
                expense.Amount = Expense.Amount;
                expense.CategoryId = SelectedCategoryId;
                _context.Transactions.Update(expense);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a Expense successfully!";
                return Redirect("/expense");
            }
            StatusMessage = "Expense not exists!";
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
