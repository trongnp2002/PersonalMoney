using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
using PersonalMoney.Models.Response;

namespace PersonalMoney.Pages.Admin
{
    public class AdminBasePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly PersonalMoneyContext _personalMoneyContext;
        protected readonly IMapper _mapper;


        public AdminBasePageModel(RoleManager<IdentityRole> roleManager, PersonalMoneyContext personalMoneyContext, IMapper mapper)
        {
            _roleManager = roleManager;
            _personalMoneyContext = personalMoneyContext;
            _mapper = mapper;
        }

        public JsonResult ResponseContent<T>(int code,string message, T data)
        {
            return new ResponseApi<T>().AsBuilder().WithSuccess(true)
                                    .WithData(data)
                                    .WithMessage(message)
                                    .WithCode(code)
                                    .Build().ConvertJson(); ;
        }
              public JsonResult ResponseContent<T>(string message, T data)
        {
            return new ResponseApi<T>().AsBuilder().WithSuccess(true)
                                    .WithData(data)
                                    .WithMessage(message)
                                    .Build().ConvertJson(); ;
        }

        public JsonResult ResponseContent<T>(T data)
        {
            return new ResponseApi<T>().AsBuilder().WithSuccess(true)
                                    .WithData(data)
                                    .WithMessage("Success! Your action was completed successfully.")
                                    .WithCode((int)HttpStatusCode.Accepted)
                                    .Build().ConvertJson(); ;
        }

        public JsonResult ResponseContent<T>(int code, T data)
        {
            return new ResponseApi<T>().AsBuilder().WithSuccess(true)
                                    .WithData(data)
                                    .WithMessage("Success! Your action was completed successfully.")
                                    .WithCode(code)
                                    .Build().ConvertJson(); ;
        }

        public JsonResult ResponseContent<T>(int code, string message)
        {
            return new ResponseApi<T>().AsBuilder().WithSuccess(true)
                                    .WithMessage(message)
                                    .WithCode(code)
                                    .Build().ConvertJson(); ;
        }

        public JsonResult ResponseOK()
        {
            return ResponseContent<Object>((int)HttpStatusCode.Accepted, "Success! Your action was completed successfully.");
        }

        public JsonResult ResponseCreated(){
            return ResponseContent<Object>((int)HttpStatusCode.Created, "Success! Your action was completed successfully."); 
        }

    }
}