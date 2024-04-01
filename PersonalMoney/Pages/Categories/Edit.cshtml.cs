using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        [BindProperty]
        public Category EditCategory { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public EditModel(PersonalMoneyContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            var category = _context.Categories.FirstOrDefault(ct => ct.Id == id);

            if (category != null)
            {
                EditCategory = category;
            }
            else
            {
                StatusMessage = "Category not exits!";
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var category = _context.Categories.FirstOrDefault(d => d.Id == EditCategory.Id);

            if (category != null)
            {
                category.Name = EditCategory.Name;
                category.IsIncome = EditCategory.IsIncome;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a Category successfully!";
                return Redirect("/categories");
            }
            StatusMessage = "Category not exists!";
            return Page();
        }
    }
}
