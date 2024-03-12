using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class BudgetModel : BasePageModel
    {
        public BudgetModel( PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

    }
}
