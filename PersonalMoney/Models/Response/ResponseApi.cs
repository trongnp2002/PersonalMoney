using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentBuilder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PersonalMoney.Models.Response
{
    [AutoGenerateBuilder]
    public class ResponseApi<T>
    {
        [JsonProperty("success")]
        public bool Success { set; get; }
        [JsonProperty("data")]
        public T? Data { set; get; }
        [JsonProperty("message")]
        public string Message { set; get; }
        [JsonProperty("error-code")]
        public int Code { set; get; }
        public JsonResult ConvertJson()
        {
            return new JsonResult(this);
        }
    }
}