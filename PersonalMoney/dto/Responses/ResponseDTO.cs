using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentBuilder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PersonalMoney.dto.Responses
{
    [AutoGenerateBuilder]
    public class ResponseDTO
    {
        [JsonProperty("success")]
        public bool Success { set; get; }
        [JsonProperty("message")]
        public string? Message { set; get; }
        [JsonProperty("data")]
        public Object? Data { set; get; }
        public ResponseDTO success(bool isSuccess)
        {
            Success = isSuccess;
            return this;
        }
        public ResponseDTO message(string message)
        {
            Message = message;
            return this;
        }
        public ResponseDTO data(Object data){
            Data = data;
            return this;
        }
        public JsonResult ConvertJson()
        {
            return new JsonResult(this);
        }
    }
}