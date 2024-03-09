using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pagesgg
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
        public DebtDetail debt { get; set; }


        public IActionResult OnGet(int id)
        {
            var de = _context.DebtDetails.FirstOrDefault(mo => mo.Id == id);

            if (de != null)
            {

                _context.DebtDetails.Remove(de);
                _context.SaveChanges();
                StatusMessage = "Deleted a debt successfully!";

            }
            else
            {
                StatusMessage = "Delete a debt failed!";
            }

            ViewData["debtorId"] = id.ToString();
            return Redirect($"/debtor/details/debt/{de.DebtorId}");
        }

    }
}
