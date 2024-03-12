using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class DebtDeatilModel : BasePageModel
    {
        public DebtDeatilModel( PersonalMoneyContext dbContext) : base( dbContext)
        {
        }


    }
}
