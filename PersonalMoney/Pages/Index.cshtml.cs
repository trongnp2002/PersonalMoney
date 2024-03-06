using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
using Microsoft.AspNetCore.Http;
namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PersonalMoneyContext _context;

        public IndexModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
            _context = dbContext;
        }

        public void OnGet()
        {
            // try
            // {
            //     throw new Exception("test");

            // }catch(Exception e){
            //     Return500ErrorPage();
            // }
            
            if (_context != null)
            {
                ViewData["listWallet"] = _context.Wallets.ToList();
            }
        }
        public IActionResult OnPostSelectWallet(int walletId)
        {
            HttpContext.Session.SetInt32("SelectedWalletId", walletId);
            return Page();
        }
    }
}
