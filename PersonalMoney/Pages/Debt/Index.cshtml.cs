using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;

namespace PersonalMoney.Pagesdebt
{
    public class IndexModel : PageModel
    {
        private readonly PersonalMoneyContext _context;

        [BindProperty]
        public DebtDetail debt { get; set; }

        [BindProperty]
        public Debtor debtor { get; set; }

        public IndexModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {

            ViewData["lstDebt"] = _context.DebtDetails
                 .Include(d => d.Debtor)
                 .Include(d => d.Wallet)
                 .Where(d => d.DebtorId == id)
                 .ToList();

            debtor = _context.Debtors.FirstOrDefault(d => d.Id == id);

        }
    }
}
