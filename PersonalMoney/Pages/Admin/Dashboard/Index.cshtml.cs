using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Pages.Admin;
using PersonalMoney.dto.UserDto;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Admin
{
    public class IndexModel : AdminBasePageModel
    {
        public List<UserDTO.AdminResponse> GroupUsers {set;get;}
        public IndexModel(RoleManager<IdentityRole> roleManager, PersonalMoneyContext personalMoneyContext, IMapper mapper) : base(roleManager, personalMoneyContext, mapper)
        {
        }

        public void OnGet()
        {
            GroupUsers = _mapper.Map<List<UserDTO.AdminResponse>>(_personalMoneyContext.Users.ToList());
        }
    }
}
