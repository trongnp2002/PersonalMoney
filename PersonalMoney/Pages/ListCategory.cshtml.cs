using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class ListCategoryModel : BasePageModel
    {
        public ListCategoryModel( PersonalMoneyContext dbContext) : base(dbContext)
        {
        }

        public void OnGet()
        {
        }
    }
}
