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

        /////////////////////////Tag's function//////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> Tag()
        {
            var tagList = await _context.Tags
                .Include(p=>p.Products)
                .Include(c=>c.CategorySupports)
                .Include(t=>t.TagSupports)
                .ToListAsync();
            return View(tagList);
        }

        public async Task<IActionResult> TagCategory(int id)
        {
            var categoryList = await _context.CategorySupport.Include(t => t.Category).Where(t => t.Tag.TagID == id).ToListAsync();
            return View(categoryList);
        }

        public async Task<IActionResult> TagProduct(int id)
        {
            var productList = await _context.Products.Include(t => t.Tag)
                .Include(c=>c.Category)
                .Where(t => t.Tag.TagID == id).ToListAsync();
            return View(productList);
        }

        public async Task<IActionResult> TagSupport(int id)
        {
            var tagSupportList = await _context.TagSupport.Include(t => t.TagDetail).Where(t => t.Tag.TagID == id).ToListAsync();
            return View(tagSupportList);
        }

        public IActionResult TagAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagAdd([Bind("TagID,TagName")] Tag tag,string[] selectedCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tag);
                    await _context.SaveChangesAsync();

                    foreach (var item in selectedCategory)
                    {
                        var tag_new = await _context.Tags.SingleOrDefaultAsync(t =>t.TagID == tag.TagID);
                         _context.CategorySupport.Add(new CategorySupport { Category = await  _context.Categories.SingleOrDefaultAsync(c=>c.CategoryName==item), Tag = tag_new });
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Tag");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(tag);
        }

        public async Task<IActionResult> TagDelete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.SingleOrDefaultAsync(p => p.TagID == id);
            if (tag == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tag);
        }

        [HttpPost, ActionName("TagDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagDeleteConfirmed(int id)
        {
            Tag tag = await _context.Tags
                 .Include(t => t.CategorySupports)
                 .Include(t=>t.TagSupports)
                 .SingleOrDefaultAsync(p => p.TagID == id);
            var categorySupportList = tag.CategorySupports.ToList();
            var tagSupportsList = tag.TagSupports.ToList();
            foreach (var categorySupport in categorySupportList)
            {
                _context.CategorySupport.Remove(categorySupport);
                await _context.SaveChangesAsync();
            }
            foreach (var tagSupports in tagSupportsList)
            {
                _context.TagSupport.Remove(tagSupports);
                await _context.SaveChangesAsync();
            }
            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();
            return RedirectToAction("Tag");
        }

        public async Task<IActionResult> TagEdit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tag tag = await _context.Tags
                                .Include(c => c.CategorySupports)
                                .Include(t=>t.TagSupports)
                                .SingleOrDefaultAsync(p => p.TagID== id);
            if (tag == null)
            {
                return NotFound();
            }
            await PopulateMCategory(id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagtEdit(int id, [Bind("TagID,TagName")] Tag tag, string[] selectedCategory)
        {
            if (id != tag.TagID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();

                    await TagUpdateCategoryAsync(id, selectedCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Tag");
            }
            return View(tag);
        }

        private async Task TagUpdateCategoryAsync(int id, string[] selectedCategory)
        {
            Tag tag =await _context.Tags.Include(c => c.CategorySupports).SingleOrDefaultAsync(t => t.TagID == id);
            foreach(var item in tag.CategorySupports)
            {
                _context.CategorySupport.Remove(item);
            }
            await _context.SaveChangesAsync();
            foreach(var item in selectedCategory)
            {
                _context.CategorySupport.Add(new CategorySupport { Tag = tag, Category = _context.Categories.SingleOrDefault(c => c.CategoryName == item) });
            }
            await _context.SaveChangesAsync();
        }

        private async Task PopulateMCategory(int id)
        {
            var allCategories = await _context.CategorySupport
                .Include(t => t.Tag)
                .Include(c => c.Category)
                .Where(t=>t.Tag.TagID==id)
                .Select(t=>t.Category.CategoryName).ToListAsync();
            ViewData["Categories"] = allCategories;
        }
        /////////////////////////Tag's function//////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> OrderDetail(int? id)
        {
            ShippingInfo order = await _context.ShippingInfos
                                    .Include(o=>o.LineItems)
                                        .ThenInclude(p=>p.Product)
                                .FirstOrDefaultAsync(s=>s.ShippingInfoID==id);
            return  View(order);
        }
        /////////////////////////Product's function//////////////////////////////////////////////////////////////////////////////////////

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

        private async Task PopulateMusicTag()
        {
            var allTags = await _context.CategorySupport
                .Include(t => t.Tag)
                .Include(c => c.Category)
                .Where(c => c.Category.CategoryName == "Music").ToListAsync();
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
        /////////////////////////Product's function//////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////TagDetail's function//////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> TagDetail()
        {
            IQueryable<TagDetail> tagDetails = _context.TagDetails.Include(t => t.TagHelper).Include(t=>t.TagSupports);
            IEnumerable<TagDetail> tagDetailsList = await tagDetails.ToListAsync();
            return View(tagDetailsList);
        }

        public async Task<IActionResult> TagDetailTag(int id)
        {
            IQueryable<TagSupport> tagSupports = _context.TagSupport.Include(t => t.Tag)
                                                                .Include(t=>t.TagDetail)
                                                    .Where(t=>t.TagDetail.TagDetailID==id);
            IEnumerable<TagSupport> tagSupportsList = await tagSupports.ToListAsync();
            return View(tagSupportsList);
        }

        public async Task<IActionResult> TagDetailProduct(int id)
        {
            IQueryable<TagHelper> tagHelper = _context.TagHelpers.Include(t => t.Product)
                                                                        .ThenInclude(c=>c.Category)
                                                                .Include(t => t.TagDetail)
                                                    .Where(t => t.TagDetail.TagDetailID == id);
            IEnumerable<TagHelper> tagHelpersList = await tagHelper.ToListAsync();
            return View(tagHelpersList);
        }

        public async Task<IActionResult> TagDetailAdd()
        {
            await populateTag(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagDEtailAdd([Bind("TagDetailID,TagDetailName")] TagDetail tagDetail,string[] selectedTag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tagDetail);
                    await _context.SaveChangesAsync();

                    foreach (var item in selectedTag)
                    {
                        var tag = await _context.Tags.SingleOrDefaultAsync(t => t.TagName == item);
                        _context.TagSupport.Add(new TagSupport { Tag = tag, TagDetail = tagDetail });
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("TagDetail");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(tagDetail);
        }

        public async Task<IActionResult> TagDetailEdit(int id)
        {
            TagDetail tagDetail = await _context.TagDetails.SingleOrDefaultAsync(t => t.TagDetailID == id);
            await populateTag(id);
            return View(tagDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagDetailEdit(int id, [Bind("TagDetailID,TagDetailName")] TagDetail tagDetail, string[] selectedTag)
        {
            if (id != tagDetail.TagDetailID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagDetail);
                    await _context.SaveChangesAsync();

                    await tagDetailUpdateTagAsync(id, selectedTag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagDetailExists(tagDetail.TagDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("TagDetail");
            }
            return View(tagDetail);
        }

        private async Task tagDetailUpdateTagAsync(int id, string[] selectedTag)
        {
            TagDetail tagDetail = await _context.TagDetails
                .Include(t=>t.TagSupports)
                .SingleOrDefaultAsync(t => t.TagDetailID == id);
            foreach(var item in tagDetail.TagSupports)
            {
                _context.TagSupport.Remove(item);
               
            }
            await _context.SaveChangesAsync();
            foreach (var item in selectedTag)
            {
                _context.TagSupport.Add(new TagSupport { Tag=await _context.Tags.SingleOrDefaultAsync(t=>t.TagName==item),TagDetail=tagDetail});
                
            }
            await _context.SaveChangesAsync();
        }

        private async Task populateTag(int? id)
        {
            var tagsAssigned = await _context.TagSupport.Include(t => t.Tag)
                .Where(t => t.TagDetail.TagDetailID == id)
                .Select(t=>t.Tag.TagName).ToListAsync();
            ViewData["TagAssigned"] = tagsAssigned;
            var tags =await  _context.Tags
                .Select(t =>t.TagName)          
                .ToListAsync();
            ViewData["Tags"] = tags;
        }

        public async Task<IActionResult> TagDetailDelete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagDetail = await _context.TagDetails.SingleOrDefaultAsync(p => p.TagDetailID == id);
            if (tagDetail == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tagDetail);
        }

        [HttpPost, ActionName("TagDetailDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TagDetailDeleteConfirmed(int id)
        {
            TagDetail tagDetail = await _context.TagDetails
                 .Include(t => t.TagSupports)
                 .Include(t=>t.TagHelper)
                 .SingleOrDefaultAsync(p => p.TagDetailID == id);
            var TagHelperList = tagDetail.TagHelper.ToList();
            foreach (var taghelper in TagHelperList)
            {
                _context.TagHelpers.Remove(taghelper);
                await _context.SaveChangesAsync();
            }

            var TagSupportList = tagDetail.TagSupports.ToList();
            foreach (var tagsupportr in TagSupportList)
            {
                _context.TagSupport.Remove(tagsupportr);
                await _context.SaveChangesAsync();
            }
            _context.TagDetails.Remove(tagDetail);

            await _context.SaveChangesAsync();
            return RedirectToAction("Product");
        }


        /////////////////////////TagDetail's function//////////////////////////////////////////////////////////////////////////////////////
        private bool ProductExists(int productID)
        {
            return _context.Products.Any(e => e.ProductID == productID);
        }
        private bool TagExists(int TagID)
        {
            return _context.Tags.Any(e => e.TagID == TagID);
        }
        private bool TagDetailExists(int TagDetailID)
        {
            return _context.TagDetails.Any(e => e.TagDetailID == TagDetailID);
        }



    }
}