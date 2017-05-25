using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicMediaWebShop.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string currentFilter, string searchString)
        {


            ViewData["CurrentFilter"] = searchString;

            var products = from s in _context.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString));
            }
            return View(await products.ToListAsync());

        }

    }
}