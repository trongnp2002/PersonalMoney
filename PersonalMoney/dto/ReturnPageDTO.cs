using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentBuilder;

namespace PersonalMoney.dto.UserDto
{
    [AutoGenerateBuilder]
    public class ReturnPageDTO<T>
    {
        public int TotalElements { set; get; }

        public int TotalPages { set; get; }

        public int PageNumber { set; get; }

        public List<T> Content { set; get; }
    }
}