using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using PersonalMoney.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PersonalMoney.Pages.Categories
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        private const int PageSize = 5;

        [BindProperty]
        public List<Category> ListCategories { get; set; }
        public PersonalMoneyContext _context { get; set; }

        [BindProperty(SupportsGet = true,Name ="pageNum")]
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
        public int TotalPages {get;set; }
        public UserManager<User> userManager;

        public IndexModel(PersonalMoneyContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }

        public async Task<IActionResult> OnGet(int? pageNum)
        {
            var user = await _userManager.GetUserAsync(User);

            IQueryable<Category> query = _dbContext.Categories.Where(c => c.UserId == user.Id);
            TotalItems = query.Count();

            CurrentPage = pageNum ?? 1;

            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);

            ListCategories = query.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }
    }
}
