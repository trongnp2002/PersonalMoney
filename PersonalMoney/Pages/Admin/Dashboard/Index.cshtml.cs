using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Pages.Admin;
using PersonalMoney.dto.UserDto;
using PersonalMoney.Models;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models.enums;
using Microsoft.AspNetCore.SignalR;
using PersonalMoney.Hubs;

namespace PersonalMoney.Pages.Admin
{
    public class IndexModel : AdminBasePageModel
    {
        public List<UserDTO.AdminResponse> GroupUsers { set; get; }
        [BindProperty(SupportsGet = true, Name = "search")]
        public string Search { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true, Name = "size")]
        public int Size { get; set; } = 10;
        [BindProperty(SupportsGet = true, Name = "pageNum")]
        public int CurrentPage { get; set; } = 1;
        public int CountPage { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<User> userManager, RoleManager<Role> roleManager, PersonalMoneyContext personalMoneyContext, IMapper mapper, IHubContext<SignalrServer> signalHub) : base(roleManager, userManager ,personalMoneyContext, mapper, signalHub)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {

            var users = await GetGroupOfUser();
            int totalElement = users.Count;
            CountPage = (int)Math.Ceiling((double)totalElement / Size);
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > CountPage) CurrentPage = CountPage;
            users = users.Skip((CurrentPage - 1) * Size).Take(Size).ToList();
            GroupUsers = users;
            return Page();
        }

        public async Task<IActionResult> OnGetData(string search, int pageNum, int size)
        {
            CurrentPage = pageNum;
            Search = String.IsNullOrEmpty(search) ? "" : search;
            Size = size;
            var users = await GetGroupOfUser();
            int totalElement = users.Count;
            CountPage = (int)Math.Ceiling((double)totalElement / Size);
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > CountPage) CurrentPage = CountPage;
            users = users.Skip((CurrentPage - 1) * Size).Take(Size).ToList();
            var responsePageDTO = new ReturnPageDTO<UserDTO.AdminResponse>().AsBuilder()
                                                .WithContent(users).WithPageNumber(pageNum)
                                                .WithTotalPages(CountPage)
                                                .WithTotalElements(totalElement).Build();
            return ResponseContent<ReturnPageDTO<UserDTO.AdminResponse>>(responsePageDTO);
        }

        public async Task<IActionResult> OnPostUpdateLockout([FromBody] InputUpdateLockout lockout)
        {
            if (String.IsNullOrEmpty(lockout.Id.Trim()))
            {
                throw new ApplicationException("Id cannot be null or empty!");
            }
            string id = lockout.Id.Trim();
            var user = await _personalMoneyContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id)) ?? throw new ApplicationException("User not exist!");
            user.LockoutEnd = null;
            user.LockoutEnabled = !user.LockoutEnabled;
            user.AccessFailedCount = 0;
            await _personalMoneyContext.SaveChangesAsync();
            return ResponseOK();
        }

        private async Task<List<UserDTO.AdminResponse>> GetGroupOfUser()
        {
            var users = await _userManager.GetUsersInRoleAsync("USER");
            return _mapper.Map<List<UserDTO.AdminResponse>>(users.Where(u => (
                (String.IsNullOrEmpty(u.Address) || u.Address.Contains(Search))
            || (String.IsNullOrEmpty(u.FirstName) || u.FirstName.Contains(Search))
            || (String.IsNullOrEmpty(u.LastName)||u.LastName.Contains(Search))
            || (String.IsNullOrEmpty(u.Email) ||u.Email.Contains(Search))
            || (String.IsNullOrEmpty(u.PhoneNumber) ||u.PhoneNumber.Contains(Search))))
            .ToList());
        }

        public class InputUpdateLockout
        {
            public string Id { set; get; }
        }

    }
}
