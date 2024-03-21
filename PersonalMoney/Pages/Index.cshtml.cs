using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }

        public void OnGet()
        {
            // try
            // {
            //     throw new Exception("test");

            // }catch(Exception e){
            //     Return500ErrorPage();
            // }
        }
    }
}
