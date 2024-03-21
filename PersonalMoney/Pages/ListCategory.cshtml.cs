using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class ListCategoryModel : BasePageModel
    {
        private readonly ILogger<ListCategoryModel> _logger;

        public ListCategoryModel(PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }

        public void OnGet()
        {
        }
    }
}
