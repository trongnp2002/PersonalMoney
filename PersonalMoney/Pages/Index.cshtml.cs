using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {   
            _logger = logger;
        }
        [BindProperty]
        public List<Wallet> ListWallets { get; set; }

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
