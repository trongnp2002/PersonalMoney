using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Expense
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Transaction Expense { get; set; }

        public DeleteModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var expense = _context.Transactions.FirstOrDefault(mo => mo.Id == id);

            if (expense != null)
            {
                Expense = expense;
            }
            else
            {
                StatusMessage = "Expense not exists!";
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (id != null)
            {
                var expense = _context.Transactions.FirstOrDefault(mo => mo.Id == id);
                if (expense != null)
                {
                    _context.Transactions.Remove(expense);
                    _context.SaveChanges();
                    StatusMessage = "Deleted a Expense successfully!";
                    return Redirect("/expense");
                }
            }
            return NotFound();
        }
    }
}
