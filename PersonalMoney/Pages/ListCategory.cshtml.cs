using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class ListCategoryModel : BasePageModel
    {
        public ListCategoryModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }

        public void OnGet()
        {
        }
    }
}
