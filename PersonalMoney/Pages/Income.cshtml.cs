using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class IncomeModel : BasePageModel
    {
        private readonly ILogger<IncomeModel> _logger;

        public IncomeModel(PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }
    }
}
