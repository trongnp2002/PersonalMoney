using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class DebtorModel : BasePageModel
    {
        public DebtorModel(PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

    }
}
