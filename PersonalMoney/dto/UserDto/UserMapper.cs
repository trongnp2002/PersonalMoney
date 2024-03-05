using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using PersonalMoney.dto.UserDto;
using PersonalMoney.Models;

namespace PersonalMoney.dto.UserDto
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO.AdminResponse>().ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.Value.ToString("dd/MM/yyyy")));
        }
    }
}