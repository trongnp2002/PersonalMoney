using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class ChangePasswordModel : BasePageModel
    {
        public ChangePasswordModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }

    }
}
