using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models.Repository
{
    public interface IProductRepository
    {
        int Count();

        Task<int> CountAsync();

        //IEnumerable<Product> Products();
        Task<IEnumerable<Product>> ProductsAsync();

        //IEnumerable<Product> Products(string category, int pageSize, int page);
        //Task<IEnumerable<Product>> ProductsAsync( int pageSize, int page);

        //Product Search(int productId);
        Task<Product> SearchAsync(int id);

        Task<IEnumerable<Product>> GetAllMusics();
    }
}
