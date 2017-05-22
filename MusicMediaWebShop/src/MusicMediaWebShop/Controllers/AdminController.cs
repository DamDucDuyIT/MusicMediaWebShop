using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MusicMediaWebShop.Models
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            IQueryable<Product> products = _context.Products.Include(t => t.Category);

            Admin admin = new Admin
            {
                Music = products.Where(p => p.Category.CategoryName.Equals("Music")).Count(),
                Film = products.Where(p => p.Category.CategoryName.Equals("Film")).Count(),
                User = _userManager.Users.Count()
            };
            return View(admin);
        }
    }
}