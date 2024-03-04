//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using PersonalMoney.Models;

//namespace PersonalMoney.Pagesg
//{
//    public class DeleteModel : PageModel
//    {
//        private readonly PersonalMoneyContext _context;
//        private readonly UserManager<User> _userManager;


//        public DeleteModel(PersonalMoneyContext context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [TempData]
//        public string StatusMessage { get; set; }

//        [BindProperty]
//        public Debtor debtor { get; set; }


//        public IActionResult OnGet(int id)
//        {
//            var debt = _context.Debtors.FirstOrDefault(mo => mo.Id == id);

//            if (debt != null)
//            {
//                debtor = debt;
//            }
//            else
//            {
//                StatusMessage = "Debtor not exists!";
//            }

//            return Page();
//        }

//        public IActionResult OnPost(int id)
//        {
//            if (id != null)
//            {
//                var p = _context.Debtors.FirstOrDefault(mo => mo.Id == id);
//                if (p != null)
//                {
//                    _context.Debtors.Remove(p);
//                    _context.SaveChanges();
//                    StatusMessage = "Deleted a debtor successfully!";
//                    return RedirectToPage("/debtor/index");
//                }
//            }

//            return NotFound();
//        }
//    }
//}
