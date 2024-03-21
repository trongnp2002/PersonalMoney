using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalMoney.dto.RoleDto;
using PersonalMoney.dto.UserDto;
using PersonalMoney.Hubs;
using PersonalMoney.Models;
using PersonalMoney.Models.enums;
using PersonalMoney.Models.Response;

namespace PersonalMoney.Pages.Admin.RolePage
{
    public class IndexModel : AdminBasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, RoleManager<Role> roleManager, UserManager<User> userManager, PersonalMoneyContext personalMoneyContext, IMapper mapper, IHubContext<SignalrServer> signalHub) : base(roleManager, userManager, personalMoneyContext, mapper, signalHub)
        {
            _logger = logger;
        }

        public List<Role> Roles { set; get; }
        public List<User> ListUser { set; get; }



        public async Task<IActionResult> OnGet()
        {
            try
            {
                Roles = await _personalMoneyContext.Roles.OrderBy(r => r.Name).ToListAsync();
                ViewData["users"] = await GetGroupOfUser();

            }
            catch (Exception e)
            {
                _logger.LogError($"🚩 | OnGet Role>>> {e.Message}");
                Return500Page();
            }
            return Page();
        }
        public IActionResult OnGetData()
        {
            try
            {
                var roles = _personalMoneyContext.Roles.ToList();
                return ResponseContent<List<Role>>(roles);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

        }
        public async Task<IActionResult> OnPostCreate([FromBody] RoleDTO roleCreate)
        {
            _logger.LogInformation($"🗝️ name| {roleCreate.Name}");
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                throw new ApplicationException(string.Join(", ", errorMessages));
            }
            var newRole = new Role(roleCreate.Name.Trim().ToUpper());
            var result = await _roleManager.CreateAsync(newRole);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => error.Description).ToList();
                throw new ApplicationException(string.Join(" ", errorMessages));
            }
            await _signalHub.Clients.All.SendAsync("ReloadRoles");
            return ResponseCreated();

        }

        public async Task<IActionResult> OnPostUpdate([FromBody] RoleDTO.Update roleUpdate)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                throw new ApplicationException(string.Join(", ", errorMessages));
            }
            var role = await _roleManager.FindByIdAsync(roleUpdate.Id) ?? throw new ApplicationException("Role Id not found");
            if (role.Name.Equals("USER") || role.Name.Equals("ADMIN")) throw new ApplicationException("This role cannot be update!");
            role.Name = roleUpdate.Name.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => error.Description).ToList();
                throw new ApplicationException(string.Join(" ", errorMessages));
            }
            await _signalHub.Clients.All.SendAsync("ReloadRoles");
            return ResponseOK();
        }

        public async Task<IActionResult> OnPostDelete([FromBody] RoleDTO roleDelete)
        {
            if (String.IsNullOrEmpty(roleDelete.Name))
            {
                throw new ApplicationException("Name cannot be null or empty!");
            }
            if (roleDelete.Name.Equals("USER") || roleDelete.Name.Equals("ADMIN")) throw new ApplicationException("This role cannot be delete!");
            var role = await _roleManager.FindByNameAsync(roleDelete.Name) ?? throw new ApplicationException("Role Id not found!");
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => error.Description).ToList();
                throw new ApplicationException(string.Join(" ", errorMessages));
            }
            await _signalHub.Clients.All.SendAsync("ReloadRoles");
            return ResponseOK();
        }

        private async Task<List<UserDTO.AdminResponse>> GetGroupOfUser()
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Name.Equals("USER"));
            var users = await _personalMoneyContext.Users.Include(u => u.Roles).Where(u => !u.Roles.Contains(role) && u.Roles.Count>0).ToListAsync();
            
            return _mapper.Map<List<UserDTO.AdminResponse>>(users).ToList();
        }

    }
}