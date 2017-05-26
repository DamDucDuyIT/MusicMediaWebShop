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
                Order = _context.ShippingInfos.Count(),
                Music = products.Where(p => p.Category.CategoryName.Equals("Music")).Count(),
                Film = products.Where(p => p.Category.CategoryName.Equals("Film")).Count(),
                User = _userManager.Users.Count()
            };
            return View(admin);
        }

        public async Task<IActionResult> Product()
        {
            IQueryable<Product> products = _context.Products.Include(t => t.Category);
            IEnumerable<Product> productsList = await products.ToListAsync();
            return View(productsList);
        }

        public async Task<IActionResult> Music()
        {
            IQueryable<Product> products = _context.Products.Include(t => t.Category);
            IEnumerable<Product> productsList = await products.Where(t => t.Category.CategoryName.Equals("Music")).ToListAsync();
            return View(productsList);
        }

        public async Task<IActionResult> Film()
        {
            IQueryable<Product> products = _context.Products.Include(t => t.Category);
            IEnumerable<Product> productsList = await products.Where(t => t.Category.CategoryName.Equals("Film")).ToListAsync();
            return View(productsList);
        }

        public async Task<IActionResult> Order()
        {
            IQueryable<ShippingInfo> shippinginfos = _context.ShippingInfos;
            IEnumerable<ShippingInfo> shippinginfosList = await shippinginfos.ToListAsync();
            return View(shippinginfosList);
        }

        public async Task<IActionResult> Users()
        {
            var userList = await _context.Users.ToListAsync();
            return View(userList);
        }

        public async Task<IActionResult> OrderDetail(int? id)
        {
            ShippingInfo order = await _context.ShippingInfos
                                    .Include(o=>o.LineItems)
                                        .ThenInclude(p=>p.Product)
                                .FirstOrDefaultAsync(s=>s.ShippingInfoID==id);
            return  View(order);
        }
    }
}