using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PersonalMoney.Models;
using PersonalMoney.Models.Response;

namespace PersonalMoney.Pages
{
    public class TestPage : BasePageModel
    {
        public TestPage( PersonalMoneyContext dbContext) : base( dbContext)
        {
        }

        public void OnGet()
        {
        }

    }
}