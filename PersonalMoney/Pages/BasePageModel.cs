using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class BasePageModel : PageModel
    {
        protected readonly ILogger<TestPage> _logger;
        protected readonly PersonalMoneyContext _dbContext;
        public BasePageModel(ILogger<TestPage> logger, PersonalMoneyContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void Return500ErrorPage(){
            Response.Redirect("/error/500");
        }
    }
}