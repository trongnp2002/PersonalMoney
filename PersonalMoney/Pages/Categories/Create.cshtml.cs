using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category AddCategory { get; set; }
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            AddCategory.UserId = user.Id;
            _context.Categories.Add(AddCategory);
            await _context.SaveChangesAsync();
            return Redirect("/categories");
        }
    }
}
