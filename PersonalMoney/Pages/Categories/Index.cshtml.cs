using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using PersonalMoney.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalMoney.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private const int PageSize = 5;

        [BindProperty]
        public List<Category> ListCategories { get; set; }
        public PersonalMoneyContext _context { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public IndexModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? page)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            IQueryable<Category> query = _context.Categories.Where(c => c.UserId == userId);

            TotalItems = query.Count();

            CurrentPage = page ?? 1;

            ListCategories = query.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Page();
        }
    }
}
