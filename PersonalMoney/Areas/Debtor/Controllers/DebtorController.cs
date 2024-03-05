//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using PersonalMoney.Models;
//using PersonalMoney.Pages;

////using DebtorModel = PersonalMoney.Models.Debtor;

//namespace AAAAAAControllers
//{
//    [Area("Debtor")]
//    [Route("debtor/[action]")]
//    public class DebtorController : Controller
//    {
//        private readonly PersonalMoneyContext _context;
//        private readonly UserManager<User> _userManager;

//        public DebtorController(PersonalMoneyContext context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [TempData]
//        public string StatusMessage { get; set; }

//        // GET: Debtor
//        // [Route("debtor/Index")]
//        public async Task<IActionResult> Index()
//        {
//            var user = await _userManager.GetUserAsync(this.User);
//            var deb = await _context.Debtors.Where(d => d.UserId == user.Id).ToListAsync();

//            return View(deb);
//        }


//        // GET: debtor/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Debtors == null)
//            {
//                return NotFound();
//            }

//            var deb = await _context.DebtDetails.Where(d => d.DebtorId == id).ToListAsync();

//            return View(deb);
//        }


//        // GET: debtor/Create
//        public async Task<IActionResult> CreateAsync()
//        {
//            //var categories = await _context.Categories.ToListAsync();
//            //ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
//            return View();
//        }

//        // POST: debtor/Create

//        [HttpPost]
//        //[ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Name,Address,Phone,Email")] DebtorModel debtor)
//        {

//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.GetUserAsync(this.User);

//                debtor.DateCreate = DateTime.Now;
//                debtor.DateUpdate = DateTime.Now;
//                debtor.UserId = user.Id;

//                _context.Debtors.Add(debtor);

//                await _context.SaveChangesAsync();
//                StatusMessage = "Add new debtor successfully!";
//                return RedirectToAction(nameof(Index));
//            }
//            return View(debtor);
//        }

//        // GET: debtor/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Debtors == null)
//            {
//                return NotFound();
//            }

//            var deb = await _context.Debtors.FirstOrDefaultAsync(c => c.Id == id);
//            if (deb == null)
//            {
//                return NotFound();
//            }
//            var debEdit = new DebtorModel()
//            {
//                Id = deb.Id,
//                Name = deb.Name,
//                Phone = deb.Phone,
//                Address = deb.Address,
//                Email = deb.Email,
//            };

//            return View(debEdit);
//        }

//        // POST: debtor/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        //[ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phone,Email")] DebtorModel debtor)
//        {

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var debUpdate = await _context.Debtors.FirstOrDefaultAsync(p => p.Id == id);

//                    if (debUpdate == null)
//                    {
//                        return NotFound();
//                    }

//                    debUpdate.Name = debtor.Name;
//                    debUpdate.Address = debtor.Address;
//                    debUpdate.Phone = debtor.Phone;
//                    debUpdate.Email = debtor.Email;
//                    debUpdate.DateUpdate = DateTime.Now;

//                    //update postCategory


//                    _context.Debtors.Update(debUpdate);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    throw new Exception();
//                }
//                StatusMessage = "Update debtor successfully.";
//                return RedirectToAction(nameof(Index));
//            }
//            return View(debtor);
//        }

//        // GET: debtor/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Debtors == null)
//            {
//                return NotFound();
//            }

//            var post = await _context.Debtors
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (post == null)
//            {
//                return NotFound();
//            }

//            return View(post);
//        }

//        // POST: debtor/Delete/5
//        [HttpPost, ActionName("Delete")]
//        //[ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Debtors == null)
//            {
//                return Problem("Entity set 'MeoShopContext.Posts'  is null.");
//            }
//            var deb = await _context.Debtors.FindAsync(id);
//            if (deb != null)
//            {
//                _context.Debtors.Remove(deb);
//            }

//            await _context.SaveChangesAsync();

//            StatusMessage = "Deleted debtor with Id: " + deb.Id;
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
