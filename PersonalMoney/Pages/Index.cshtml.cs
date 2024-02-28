﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalMoney.Models;
namespace PersonalMoney.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PersonalMoneyContext _context;



        public IndexModel(ILogger<IndexModel> logger, PersonalMoneyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
