using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.dto.Responses;
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

        public JsonResult GetResponseJson(ResponseDTO response)
        {
            return response.ConvertJson();
        }

          public JsonResult GetResponseJson(bool isSuccess, string message, Object data)
        {
            return new ResponseDTO().success(isSuccess).message(message)
                            .data(data).ConvertJson();
        }

        public void Return500ErrorPage(){
            Response.Redirect("/error/500");
        }
    }
}