using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {

        [BindProperty]
        public List<Wallet> ListWallets { get; set; }
        public IndexModel( PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

        public IActionResult OnGet()
        {
            ListWallets = _dbContext.Wallets.ToList();
            return Page();
        }

        public IActionResult OnPost(int id)
        { 
            var wallet = _dbContext.Wallets.Find(id);
            if (wallet != null)
            {
                _dbContext.Wallets.Remove(wallet);
                _dbContext.SaveChanges();
            }
            return Page();
        }
    }
}
