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

        public async Task<IActionResult> ProductAdd()
        {
            await PopulateMusicTag();
            await PopulateFilmTag();                
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductAdd([Bind("ProductID,ProductDescription,ProductName,Price")] Product product, string selectedCategory, string selectedTag, string[] tagHelper)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.Category = _context.Categories.FirstOrDefault(c => c.CategoryName == selectedCategory);
                    product.Tag = _context.Tags.FirstOrDefault(c => c.TagName == selectedTag);
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    foreach (var item in tagHelper)
                    {
                        var tagDetail = await _context.TagDetails.SingleOrDefaultAsync(t => t.TagDetailName == item);
                        _context.TagHelpers.Add(new TagHelper { Product = product, TagDetail = tagDetail });
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Product");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(product);
        }
        public async Task<IActionResult> ProductEdit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _context.Products
                                .Include(c=>c.Category)
                                .SingleOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            await PopulateMusicTag();
            await PopulateFilmTag();
            
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(int id, [Bind("ProductID,ProductDescription,ProductName,Price")] Product product, string selectedCategory,string selectedTag, string[] tagHelper)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    product.Category = _context.Categories.FirstOrDefault(c => c.CategoryName == selectedCategory);
                    product.Tag = _context.Tags.FirstOrDefault(c => c.TagName == selectedTag);
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    await StudentUpdateTagAsync(id,tagHelper);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Product");
            }
            return View(product);
        }

        private async Task StudentUpdateTagAsync(int id, string[] tagHelper)
        {
            var product = await _context.Products
                .Include(t=>t.TagHelper)
                .SingleOrDefaultAsync(p => p.ProductID == id);
            foreach (var item in product.TagHelper)
            {
                _context.TagHelpers.Remove(item);
            }
            await _context.SaveChangesAsync();

            foreach (var item in tagHelper)
            {
                var tagDetail = await _context.TagDetails.SingleOrDefaultAsync(t => t.TagDetailName == item);
                _context.TagHelpers.Add(new TagHelper { Product = product, TagDetail = tagDetail });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           Product product = await _context.Products
                .Include(t=>t.TagHelper)
                .SingleOrDefaultAsync(p => p.ProductID == id);
            var TagHelperList = product.TagHelper.ToList();
            foreach (var taghelper in TagHelperList)
            {
                _context.TagHelpers.Remove(taghelper);
                await _context.SaveChangesAsync();
            }
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction("Product");
        }

        [HttpPost]
        public JsonResult GetTagSupport(String TagName,int? id)
        {
            var allTags = _context.TagHelpers
                            .Include(t => t.TagDetail)
                            .Include(p => p.Product)
                            .Where(p => p.Product.ProductID == id).Select(t => t.TagDetail.TagDetailName).ToList();
            var tagSupportList = _context.TagSupport
                            .Include(t => t.Tag)
                            .Include(t=>t.TagDetail)
                            .Where(t => t.Tag.TagName == TagName)
                            .Select(t=>new {
                                tagDetailName=t.TagDetail.TagDetailName,
                                assigned = allTags.Contains(t.TagDetail.TagDetailName)
                            })
                            .ToList();
            return Json(new
            {
                list=tagSupportList
            });
        }

        private bool ProductExists(int productID)
        {
            return _context.Products.Any(e => e.ProductID == productID);
        }

        private async Task PopulateMusicTag()
        {
            var allTags = await _context.CategorySupport
                .Include(t=>t.Tag)
                .Include(c=>c.Category)
                .Where(c=>c.Category.CategoryName=="Music").ToListAsync();
            ViewData["MusicTags"] = allTags;
        }

        private async Task PopulateFilmTag()
        {
            var allTags = await _context.CategorySupport
                .Include(t => t.Tag)
                .Include(c => c.Category)
                .Where(c => c.Category.CategoryName == "Film").ToListAsync();
            ViewData["FilmTags"] = allTags;
        }


    }
}