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
using Microsoft.Extensions.Logging;
using PersonalMoney.Models;
using PersonalMoney.Models.Response;

namespace PersonalMoney.Pages.Admin.Role
{
    public class Index : AdminBasePageModel
    {
        private readonly ILogger<Index> _logger;
        public List<IdentityRole> Roles { set; get; }
        // [BindProperty]
        // public InputModel Input { set; get; }
        public Index(ILogger<Index> logger, RoleManager<IdentityRole> roleManager, PersonalMoneyContext personalMoneyContext, IMapper mapper) : base(roleManager, personalMoneyContext, mapper)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Roles = _personalMoneyContext.Roles.ToList();
        }
        public IActionResult OnGetData()
        {
            try
            {
                var roles =  _personalMoneyContext.Roles.ToList();
                return ResponseContent<List<IdentityRole>>(roles);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

        }
        public async Task<IActionResult> OnPostCreate([FromBody] InputModel inputModel)
        {
            _logger.LogInformation($"🗝️ name| {inputModel.Name}");
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                throw new ApplicationException(string.Join(", ", errorMessages));
            }
            var newRole = new IdentityRole(inputModel.Name.Trim());
            var result = await _roleManager.CreateAsync(newRole);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => error.Description).ToList();
                throw new ApplicationException(string.Join(" ", errorMessages));
            }
            return new ResponseApi<IdentityRole>().AsBuilder().WithSuccess(true)
                                    .WithData(newRole)
                                    .WithMessage("Success! Your action was completed successfully.")
                                    .WithCode((int)HttpStatusCode.Created)
                                    .Build().ConvertJson();

        }

        public async Task<IActionResult> OnPostUpdate([FromBody] InputUpdateModel inputUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                throw new ApplicationException(string.Join(", ", errorMessages));
            }
            var role = await _roleManager.FindByIdAsync(inputUpdateModel.Id);
            if (role == null)
            {
                throw new ApplicationException("Role Id not found");
            }

            role.Name = inputUpdateModel.Name.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => error.Description).ToList();
                throw new ApplicationException(string.Join(" ", errorMessages));
            }

            return new ResponseApi<IdentityRole>().AsBuilder().WithSuccess(true)
                   .WithData(role)
                   .WithMessage("Success! Your action was completed successfully.")
                   .WithCode((int)HttpStatusCode.Accepted)
                   .Build().ConvertJson();
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["error"] = "Role Id not found";
            }
            else
            {

                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(error =>
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    });
                }
                else
                {
                    return RedirectToPage();
                }

            }

            Roles = _personalMoneyContext.Roles.ToList();
            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} cannot be empty")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must has from {2} to {1} character length")]
            public string Name { set; get; }
        }

        public class InputUpdateModel
        {
            [Required(ErrorMessage = "{0} cannot be empty")]
            public string Id { set; get; }
            [Required(ErrorMessage = "{0} cannot be empty")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must has from {2} to {1} character length")]
            public string Name { set; get; }
        }
    }
}