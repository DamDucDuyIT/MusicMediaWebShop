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

        public async Task<IEnumerable<Product>> GetAllFilms() => await _context.Products
                                                                                .Include(p => p.Category)
                                                                                .Where(p => p.Category.CategoryName.Equals("Film"))
                                                                                .ToListAsync();


        public async Task<IEnumerable<Product>> GetAllMusics() => await _context.Products
                                                                                .Include(p => p.Category)
                                                                                .Where(p => p.Category.CategoryName.Equals("Music"))
                                                                                .ToListAsync();
        

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
