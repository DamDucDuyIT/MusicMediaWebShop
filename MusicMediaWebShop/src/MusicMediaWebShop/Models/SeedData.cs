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

            Tag Kpop = new Tag { TagName = "K-POP" };
            Tag Vpop = new Tag { TagName = "V-POP" };
            Tag Jpop = new Tag { TagName = "J-POP" };
            Tag Usuk = new Tag { TagName = "US-UK" };
            Tag Series = new Tag { TagName = "Series" };
            Tag Movie = new Tag { TagName = "Movie" };
            Tag Cartoon = new Tag { TagName = "Cartoon" };
            Tag Anime = new Tag { TagName = "Anime" };
            context.Tags.AddRange(Kpop, Vpop, Jpop, Usuk,Series,Movie,Cartoon,Anime);
            context.SaveChanges();

            TagDetail Rock = new TagDetail { TagDetailName = "Rock" };
            TagDetail Pop = new TagDetail { TagDetailName = "Pop" };
            TagDetail Country = new TagDetail { TagDetailName = "Country" };
            TagDetail Dance = new TagDetail { TagDetailName = "Dance" };
            TagDetail Rap = new TagDetail { TagDetailName = "Rap" };
            TagDetail RBSoul = new TagDetail { TagDetailName = "RBSoul" };
            TagDetail Action = new TagDetail { TagDetailName = "Action" };
            TagDetail Comedy = new TagDetail { TagDetailName = "Comedy" };
            TagDetail Horror = new TagDetail { TagDetailName = "Horror" };
            TagDetail Fantasy = new TagDetail { TagDetailName = "Fantasy" };
            TagDetail Romance = new TagDetail { TagDetailName = "Romance" };
            TagDetail Sport = new TagDetail { TagDetailName = "Sport" };
            TagDetail Funny = new TagDetail { TagDetailName = "Funny" };
            TagDetail SuperHero = new TagDetail { TagDetailName = "SuperHero" };
            TagDetail Education = new TagDetail { TagDetailName = "Education" };
            TagDetail Harem = new TagDetail { TagDetailName = "Harem" };
            TagDetail Casual = new TagDetail { TagDetailName = "Casual" };
            TagDetail Shounen = new TagDetail { TagDetailName = "Shounen" };
            context.TagDetails.AddRange(Rock,Pop,Country,Dance,Rap,RBSoul,Action,Comedy,Horror,
                Fantasy,Romance,Sport,Funny,SuperHero,Education,Harem,Casual,Shounen);
            context.SaveChanges();

            Category music = new Category { CategoryName = "Music" };
            Category film = new Category { CategoryName = "Film" };
            context.Categories.AddRange(music, film);
            context.SaveChanges();

            context.Products.AddRange(
                new Product { ProductName = "Attack on Titan", Category = film, Price = 19.50m, Tag = Anime},
                new Product { ProductName = "Fast and Furious 8", Category = film, Price = 20.50m, Tag = Movie },
                new Product { ProductName = "Yêu là tha thu", Category = music, Price = 9.50m, Tag = Vpop },
                new Product { ProductName = "Knock Knock", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "John Wick", Category = music, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "Drake Illo Fall", Category = music, Price = 44.35m, Tag = Usuk },
                new Product { ProductName = "Bad Blood", Category = music, Price = 2.50m, Tag = Usuk },
                new Product { ProductName = "Death Note", Category = film, Price = 100.50m, Tag = Anime },
                new Product { ProductName = "Tokyo Ghoul", Category = film, Price = 200m, Tag = Anime },
                new Product { ProductName = "Big Hero 6", Category = film, Price = 1.50m, Tag = Cartoon },
                new Product { ProductName = "Tom and Jerry", Category = film, Price = 18.50m, Tag = Cartoon },
                new Product { ProductName = "Pokemon: Magiana", Category = film, Price = 58.69m, Tag = Anime },
                new Product { ProductName = "Jason Bourne", Category = film, Price = 69.69m, Tag = Movie }               
            );
            context.SaveChanges();

            context.TagHelpers.AddRange(
                new TagHelper { Product = context.Products.FirstOrDefault(p=>p.ProductName== "Attack on Titan"),TagDetail=Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Attack on Titan"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Attack on Titan"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fast and Furious 8"), TagDetail = Romance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fast and Furious 8"), TagDetail = Comedy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fast and Furious 8"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Yêu là tha thu"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Knock Knock"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Knock Knock"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "John Wick"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "John Wick"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Drake Illo Fall"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Drake Illo Fall"), TagDetail = RBSoul },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bad Blood"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Death Note"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tokyo Ghoul"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tokyo Ghoul"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tokyo Ghoul"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Big Hero 6"), TagDetail = Funny },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Big Hero 6"), TagDetail = Education },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Big Hero 6"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tom and Jerry"), TagDetail = Funny },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tom and Jerry"), TagDetail = Education },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Pokemon: Magiana"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Pokemon: Magiana"), TagDetail = Funny },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Jason Bourne"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Jason Bourne"), TagDetail = Funny }
                );
            context.SaveChanges();
        }
    }
}
