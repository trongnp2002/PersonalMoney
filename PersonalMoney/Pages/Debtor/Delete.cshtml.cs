using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pagesg
{
    public class DeleteModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;


        public DeleteModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Debtor debtor { get; set; }


        public IActionResult OnGet(int id)
        {
            var debt = _context.Debtors.FirstOrDefault(mo => mo.Id == id);

            if (debt != null)
            {
                _context.Debtors.Remove(debt);
                _context.SaveChanges();
                StatusMessage = "Deleted a debtor successfully!";
            }
            else
            {
                StatusMessage = "Debtor not exists!";
            }

            return RedirectToPage("/debtor/index");
        }

    }
}
