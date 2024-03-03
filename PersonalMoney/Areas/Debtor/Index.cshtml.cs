using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;

namespace PersonalMoney.Pagess
{
    public class IndexModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public Debtor debtor { get; set; }
        public IndexModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            var user = _userManager.GetUserAsync(this.User);
            ViewData["lstDebtors"] = _context.Debtors.Where(d => d.UserId == user.Id.ToString()).ToListAsync();

        }
    }
}
