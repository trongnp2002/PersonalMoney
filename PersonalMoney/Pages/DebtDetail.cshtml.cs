using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class DebtDeatilModel : BasePageModel
    {
        public DebtDeatilModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }


    }
}
