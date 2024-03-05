using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Page
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

        [BindProperty]
        public Debtor debtor { get; set; }


        public IActionResult OnGet(int id)
        {
            debtor = _context.Debtors.FirstOrDefault(mo => mo.Id == id);

            if (debtor == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (id != null)
            {
                var p = _context.Debtors.FirstOrDefault(mo => mo.Id == id);
                if (p != null)
                {
                    _context.Debtors.Remove(p);
                    _context.SaveChanges();
                    return RedirectToPage("/index");
                }
            }

            return NotFound();
        }
    }
}
