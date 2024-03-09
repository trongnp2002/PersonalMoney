using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonalMoney.Models;
using System.Drawing.Printing;

namespace PersonalMoney.Pagesdebt
{
    public class IndexModel : PageModel
    {
        private readonly PersonalMoneyContext _context;

        [BindProperty]
        public DebtDetail debt { get; set; }

        [BindProperty]
        public Debtor debtor { get; set; }

        [BindProperty]
        public string FilterType { get; set; }
        [BindProperty]
        public string FilterValueStart { get; set; }
        [BindProperty]
        public string FilterValueEnd { get; set; }

        public IndexModel(PersonalMoneyContext context)
        {
            _context = context;
        }

        public void OnGet(int id, [FromQuery(Name = "p")] int currentPage, int pageSize)
        {
            var lst = _context.DebtDetails
                 .Include(d => d.Debtor)
                 .Include(d => d.Wallet)
                 .Where(d => d.DebtorId == id)
                 .OrderBy(d => d.DateDebt)
                 .ToList();

            int totalPosts = lst.Count();
            if (pageSize <= 0) pageSize = 5;
            int countPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pageSize = pageSize
                })
            };

            ViewData["pagingModel"] = pagingModel;
            ViewData["totalPosts"] = totalPosts;
            ViewData["postIndex"] = (currentPage - 1) * pageSize;
            var postInPage = lst.Where(d => d.DebtorId == id)
                      .OrderBy(d => d.DateDebt)
                      .Skip((currentPage - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();

            ViewData["lstDebt"] = postInPage;
            debtor = _context.Debtors.FirstOrDefault(d => d.Id == id);
            FilterType = "total";
            ViewData["ids"] = id;

        }

        public IActionResult OnPost()
        {
            var id = Request.Form["id"];
            FilterType = Request.Form["filterType"];
            FilterValueStart = Request.Form["filterValueStart"];
            FilterValueEnd = Request.Form["filterValueEnd"];
            if (FilterValueStart != "" && FilterValueEnd != "")
            {
                if (FilterType.Equals("total"))
                {
                    var lst = _context.DebtDetails
                  .Include(d => d.Debtor)
                  .Include(d => d.Wallet)
                  .Where(d => d.DebtorId == int.Parse(id) && d.MoneyDebt >= decimal.Parse(FilterValueStart) && d.MoneyDebt <= decimal.Parse(FilterValueEnd))
                  .ToList();
                    ViewData["lstDebt"] = lst;
                }
                else if (FilterType.Equals("date"))
                {
                    var lst = _context.DebtDetails
                   .Include(d => d.Debtor)
                   .Include(d => d.Wallet)
                   .Where(d => d.DebtorId == int.Parse(id) && d.DateDebt >= DateTime.Parse(FilterValueStart) && d.DateDebt <= DateTime.Parse(FilterValueEnd))
                   .ToList();
                    ViewData["lstDebt"] = lst;
                }
            }
            else
            {
                var lst = _context.DebtDetails
               .Include(d => d.Debtor)
               .Include(d => d.Wallet)
               .Where(d => d.DebtorId == int.Parse(id))
               .ToList();

                ViewData["lstDebt"] = lst;
            }

            debtor = _context.Debtors.FirstOrDefault(d => d.Id == int.Parse(id));
            ViewData["ids"] = int.Parse(id);
            return Page();
        }
    }
}

