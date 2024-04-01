using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;

namespace PersonalMoney.Pages.Categories
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Category DeleteCategory { get; set; }

        public DeleteModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var category = _context.Categories.FirstOrDefault(ct => ct.Id == id);

            if (category != null)
            {
                DeleteCategory = category;
            }
            else
            {
                StatusMessage = "Category not exists!";
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (id != null)
            {
                var c = _context.Categories.FirstOrDefault(ct => ct.Id == id);
                if (c != null)
                {
                    _context.Categories.Remove(c);
                    _context.SaveChanges();
                    StatusMessage = "Deleted a Category successfully!";
                    return Redirect("/categories");
                }
            }
            return NotFound();
        }
    }
}
