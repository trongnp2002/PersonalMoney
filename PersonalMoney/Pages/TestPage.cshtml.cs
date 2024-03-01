using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PersonalMoney.dto.Responses;
using PersonalMoney.Models;

namespace PersonalMoney.Pages
{
    public class TestPage : BasePageModel
    {
        public TestPage(ILogger<TestPage> logger, PersonalMoneyContext dbContext) : base(logger, dbContext)
        {
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetTest(string value)
        {
            if (value.Equals("fail"))
            {
                throw new ApplicationException("EError");
            }
            var response = new ResponseDTO().message("thanh-cong").success(true).data(_dbContext.Users.ToList());
            var response2 = new ResponseDTO().AsBuilder().WithSuccess(true).WithMessage("thanh cong").Build();
            return GetResponseJson(response);
        }
    }
}