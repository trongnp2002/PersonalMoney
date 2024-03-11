using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class IncomeModel : BasePageModel
    {
        public IncomeModel(PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

    }
}
