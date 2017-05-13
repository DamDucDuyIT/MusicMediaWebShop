using Microsoft.EntityFrameworkCore;
using MusicMediaWebShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models.Repository
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Count() => _context.Products.Count();

        public async Task<int> CountAsync() => await _context.Products.CountAsync();

        public async Task<IEnumerable<Product>> GetAllFilms(string TagDetail, string Tag)
        {
            if (String.IsNullOrEmpty(Tag) && String.IsNullOrEmpty(TagDetail))   //trường hợp user click vào category
            {              
                return await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Category.CategoryName.Equals("Film"))
                    .ToListAsync();
            }
            else if (String.IsNullOrEmpty(TagDetail))                           //trường hợp user click vào các category cha
            {
                return await _context.Products
              .Include(p => p.Category)
              .Include(t => t.TagHelper)
                  .ThenInclude(ta => ta.TagDetail)
              .Where(p => p.Category.CategoryName.Equals("Film") && p.Tag.TagName.Equals(Tag))
              .ToListAsync();
            }
            else                                                               //trường hợp user click vào các category con
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .Include(t => t.TagHelper)
                        .ThenInclude(ta => ta.TagDetail)
                    .Where(p => p.Category.CategoryName.Equals("Film") && p.Tag.TagName.Equals(Tag)
                        && p.TagHelper.Any(t => t.TagDetail.TagDetailName == TagDetail))
                    .ToListAsync();
            }
        }


        public async Task<IEnumerable<Product>> GetAllMusics(string TagDetail, string Tag)
        {
            if(String.IsNullOrEmpty(Tag) && String.IsNullOrEmpty(TagDetail))   //trường hợp user click vào category
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Category.CategoryName.Equals("Music"))
                    .ToListAsync();
            }
            else if (String.IsNullOrEmpty(TagDetail))                           //trường hợp user click vào các category cha
            {
                return await _context.Products
              .Include(p => p.Category)
              .Include(t => t.TagHelper)
                  .ThenInclude(ta => ta.TagDetail)
              .Where(p => p.Category.CategoryName.Equals("Music") && p.Tag.TagName.Equals(Tag))
              .ToListAsync();
            }
            else                                                               //trường hợp user click vào các category con
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .Include(t=>t.TagHelper)
                        .ThenInclude(ta=>ta.TagDetail)
                    .Where(p => p.Category.CategoryName.Equals("Music") && p.Tag.TagName.Equals(Tag)
                        && p.TagHelper.Any(t => t.TagDetail.TagDetailName == TagDetail))
                    .ToListAsync();
            }

        }

        public Task<IEnumerable<Product>> ProductsAsync()
        {
            throw new NotImplementedException();
        }
        

        //public async Task<IEnumerable<Product>> ProductsAsync(int pageSize, int page) => await _context.Products.ToListAsync();


        public async Task<Product> SearchAsync(int id) =>
            await _context.Products
            .FirstOrDefaultAsync(p => p.ProductID == id);
    }
}
