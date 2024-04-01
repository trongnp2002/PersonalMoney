using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Income
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Transaction Income { get; set; }

        public DeleteModel(PersonalMoneyContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            var income = _context.Transactions.FirstOrDefault(mo => mo.Id == id);

            if (income != null)
            {
                Income = income;
            }
            else
            {
                StatusMessage = "Income not exists!";
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (id != null)
            {
                var income = _context.Transactions.FirstOrDefault(mo => mo.Id == id);
                if (income != null)
                {
                    _context.Transactions.Remove(income);
                    _context.SaveChanges();
                    StatusMessage = "Deleted a Income successfully!";
                    return Redirect("/income");
                }
            }
            return NotFound();
        }
    }
}
