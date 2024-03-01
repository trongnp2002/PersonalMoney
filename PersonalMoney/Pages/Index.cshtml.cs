using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PersonalMoneyContext _context;

        public IndexModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }

        public void OnGet()
        {
            try
            {
                throw new ApplicationException();

            }catch(Exception e){
                Return500ErrorPage();
            }
        }
    }
}
