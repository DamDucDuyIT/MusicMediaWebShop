using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MusicMediaWebShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
              .GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return;
            }

            Category music = new Category { CategoryName = "Music" };
            Category film = new Category { CategoryName = "Film" };

            context.Categories.AddRange(music, film);
            context.SaveChanges();

            context.Products.AddRange(
                new Product { ProductName = "Attack on Titan", Category = film, Price = 19.50m },
                new Product { ProductName = "Fast and Furious 8", Category = film, Price = 20.50m },
                new Product { ProductName = "Yêu là tha thu", Category = music, Price = 9.50m },
                new Product { ProductName = "Knock Knock", Category = music, Price = 5.60m },
                new Product { ProductName = "John Wick", Category = music, Price = 143.50m },
                new Product { ProductName = "Drake Illo Fall", Category = music, Price = 44.35m },
                new Product { ProductName = "Bad Blood", Category = music, Price = 2.50m },
                new Product { ProductName = "Death Note", Category = film, Price = 100.50m },
                new Product { ProductName = "Tokyo Ghoul", Category = film, Price = 200m },
                new Product { ProductName = "Big Hero 6", Category = film, Price = 1.50m },
                new Product { ProductName = "Tom and Jerry", Category = film, Price = 18.50m },
                new Product { ProductName = "Pokemon: Magiana", Category = film, Price = 58.69m },
                new Product { ProductName = "jason Bourne", Category = film, Price = 69.69m }               
            );
            context.SaveChanges();
        }
    }
}
