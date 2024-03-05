using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using System;
using System.Threading.Tasks;

namespace PersonalMoney.Pageg
{
    public class EditModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public Debtor debtor { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public EditModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet(int id)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            List<Debtor> lst = new List<Debtor>();


            var deb = _context.Debtors.FirstOrDefault(d => d.Id == id);

            if (deb != null)
            {
                debtor = deb;
            }
            else
            {
                StatusMessage = "Debtor not exits!";
            }

        }


        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var debUpdate = _context.Debtors.FirstOrDefault(d => d.Id == debtor.Id);

            if (debUpdate != null)
            {
                debUpdate.Name = debtor.Name;
                debUpdate.Address = debtor.Address;
                debUpdate.Phone = debtor.Phone;
                debUpdate.Email = debtor.Email;

                debUpdate.DateUpdate = DateTime.Now;
                _context.Debtors.Update(debUpdate);
                await _context.SaveChangesAsync();
                StatusMessage = "Update a debtor successfully!";
                return RedirectToPage("/debtor/index");
            }

            StatusMessage = "Debtor not exists!";
            return Page();
        }
    }
}
