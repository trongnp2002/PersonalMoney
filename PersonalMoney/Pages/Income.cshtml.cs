using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class IncomeModel : BasePageModel
    {
        public IncomeModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }

    }
}
