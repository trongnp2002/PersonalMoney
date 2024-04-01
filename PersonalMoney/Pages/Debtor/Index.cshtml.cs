using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using PersonalMoney.Models;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace PersonalMoney.Pagesedddd
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PersonalMoneyContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public Debtor debtor { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string FilterType { get; set; }
        [BindProperty]
        public string FilterValueStart { get; set; }
        [BindProperty]
        public string FilterValueEnd { get; set; }
        public IndexModel(PersonalMoneyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet([FromQuery(Name = "p")] int currentPage, int pageSize)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            var lst = _context.Debtors.Where(d => d.UserId == user.Id.ToString()).ToList();

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
            var postInPage = lst.Where(d => d.UserId == user.Id.ToString())
                      .Skip((currentPage - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();

            ViewData["lstDebtors"] = postInPage;
            FilterType = "total";
        }

        public IActionResult OnPost([FromQuery(Name = "p")] int currentPage, int pageSize)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var lst = new List<Debtor>();
            FilterType = Request.Form["filterType"];
            FilterValueStart = Request.Form["filterValueStart"];
            FilterValueEnd = Request.Form["filterValueEnd"];

            //if ((FilterType != "name") && ((FilterValueStart != "" && FilterValueStart == "") || (FilterValueStart == "" && FilterValueStart != "")))
            //{
            //    StatusMessage = "Please enter both From and To values before submitting the form.";
            //    lst = _context.Debtors.Where(d => d.UserId == user.Id.ToString()).ToList();
            //}
            //else
            //{
            //    if ((FilterValueStart != "" && FilterValueEnd != "") || (FilterValueStart != "" && FilterValueEnd == ""))
            //    {
            //        if (FilterType.Equals("total"))
            //        {
            //            lst = _context.Debtors
            //         .Where(d => d.UserId == user.Id.ToString() && d.TotalMoney >= decimal.Parse(FilterValueStart) && d.TotalMoney <= decimal.Parse(FilterValueEnd))
            //         .ToList();
            //        }
            //        else if (FilterType.Equals("date"))
            //        {
            //            lst = _context.Debtors
            //           .Where(d => d.UserId == user.Id.ToString() && d.DateCreate >= DateTime.Parse(FilterValueStart) && d.DateCreate <= DateTime.Parse(FilterValueEnd))
            //           .ToList();
            //        }
            //        else if (FilterType.Equals("name"))
            //        {
            //            lst = _context.Debtors
            //          .Where(d => d.UserId == user.Id.ToString() && d.Name.Contains(FilterValueStart.ToString()))
            //          .ToList();
            //        }
            //    }
            //    else
            //    {
            //        lst = _context.Debtors.Where(d => d.UserId == user.Id.ToString()).ToList();
            //    }
            //}

            if (FilterType != "name")
            {
                if ((FilterValueStart != "" && FilterValueEnd == "") || (FilterValueStart == "" || FilterValueEnd != ""))
                {
                    // Nếu FilterType không phải là "name" và ít nhất một trong hai trường FilterValueStart và FilterValueEnd không được điền
                    StatusMessage = "Please enter both From and To values before submitting the form.";
                    lst = _context.Debtors.Where(d => d.UserId == user.Id.ToString()).ToList();
                }
                else
                {
                    // Nếu cả hai trường FilterValueStart và FilterValueEnd đều đã được điền
                    if (FilterType == "total" && decimal.TryParse(FilterValueStart, out var startValue) && decimal.TryParse(FilterValueEnd, out var endValue))
                    {
                        lst = _context.Debtors
                            .Where(d => d.UserId == user.Id.ToString() && d.TotalMoney >= startValue && d.TotalMoney <= endValue)
                            .ToList();
                    }
                    else if (FilterType == "date" && DateTime.TryParse(FilterValueStart, out var startDate) && DateTime.TryParse(FilterValueEnd, out var endDate))
                    {
                        lst = _context.Debtors
                            .Where(d => d.UserId == user.Id.ToString() && d.DateCreate >= startDate && d.DateCreate <= endDate)
                            .ToList();
                    }
                }
            }
            else
            {
                // Trường hợp FilterType là "name"
                if (FilterValueStart != "")
                {
                    lst = _context.Debtors
                        .Where(d => d.UserId == user.Id.ToString() && d.Name.Contains(FilterValueStart))
                        .ToList();
                    FilterType = "name";
                }
                else
                {
                    lst = _context.Debtors
                        .Where(d => d.UserId == user.Id.ToString())
                        .ToList();
                }
            }

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
            var postInPage = lst.Where(d => d.UserId == user.Id.ToString())
                      .Skip((currentPage - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();
            ViewData["lstDebtors"] = postInPage;
            return Page();
        }
    }
}
