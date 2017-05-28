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
                new Product { ProductName = "Dawn of the apocalypse", Category = music, Price = 12.60m, Tag = Kpop },
                new Product { ProductName = "Awakening", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "On raining days", Category = music, Price = 12.60m, Tag = Kpop },
                new Product { ProductName = "Wedding dress", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "Cry cry", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "Day By Day", Category = music, Price = 12.60m, Tag = Kpop },
                new Product { ProductName = "My Country is the best", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "Women are Flowers", Category = music, Price = 12.60m, Tag = Kpop },
                new Product { ProductName = "Your scent", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "Bicycle", Category = music, Price = 12.60m, Tag = Kpop },
                new Product { ProductName = "10 Minutes", Category = music, Price = 5.60m, Tag = Kpop },
                new Product { ProductName = "Holding onto the End of the Night", Category = music, Price = 5.60m, Tag = Kpop },

                new Product { ProductName = "In the end", Category = music, Price = 12.60m, Tag = Usuk },
                new Product { ProductName = "Waiting for the end", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "When I was your man", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "Baby", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "Something just like this", Category = music, Price = 12.60m, Tag = Usuk },
                new Product { ProductName = "Closer", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "Love story", Category = music, Price = 12.60m, Tag = Usuk },
                new Product { ProductName = "Fifteen", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "Timber", Category = music, Price = 12.60m, Tag = Usuk },
                new Product { ProductName = "The monster", Category = music, Price = 5.60m, Tag = Usuk },
                new Product { ProductName = "Criminal", Category = music, Price = 12.60m, Tag = Usuk },
                new Product { ProductName = "The day you went away", Category = music, Price = 5.60m, Tag = Usuk },

                new Product { ProductName = "Chỉ là giấc mơ", Category = music, Price = 12.60m, Tag = Vpop, ProductURL = "ChiLaGiacMo.mp3" },
                new Product { ProductName = "Tâm hồn của đá", Category = music, Price = 5.60m, Tag = Vpop, ProductURL = "TamHonCuaDa.mp3" },
                new Product { ProductName = "Yêu là tha thu", Category = music, Price = 12.60m, Tag = Vpop, ProductURL = "YeuLaThaThu.mp3" },
                new Product { ProductName = "Đã từng vô giá", Category = music, Price = 5.60m, Tag = Vpop, ProductURL = "DaTungVoGia.mp3" },
                new Product { ProductName = "Con bướm xuân", Category = music, Price = 5.60m, Tag = Vpop },
                new Product { ProductName = "Let me be your love", Category = music, Price = 12.60m, Tag = Vpop },
                new Product { ProductName = "Bà tôi", Category = music, Price = 5.60m, Tag = Vpop },
                new Product { ProductName = "Làng quan họ quê tôi", Category = music, Price = 12.60m, Tag = Vpop },
                new Product { ProductName = "Đưa nhau đi trốn", Category = music, Price = 5.60m, Tag = Vpop },
                new Product { ProductName = "Quăng tao cái boong", Category = music, Price = 12.60m, Tag = Vpop },
                new Product { ProductName = "Bâng khuâng", Category = music, Price = 5.60m, Tag = Vpop },
                new Product { ProductName = "Em đã từng", Category = music, Price = 5.60m, Tag = Vpop },

                new Product { ProductName = "Senbonzakura", Category = music, Price = 12.60m, Tag = Jpop },
                new Product { ProductName = "TOP TEN", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "Oboduky", Category = music, Price = 12.60m, Tag = Jpop },
                new Product { ProductName = "Magnet", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "Hanahaodori", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "Transfer", Category = music, Price = 12.60m, Tag = Jpop },
                new Product { ProductName = "Whisper of the heart", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "A fool such as l", Category = music, Price = 12.60m, Tag = Jpop },
                new Product { ProductName = "It G Ma", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "Iruka", Category = music, Price = 12.60m, Tag = Jpop },
                new Product { ProductName = "Missing", Category = music, Price = 5.60m, Tag = Jpop },
                new Product { ProductName = "P.P.M", Category = music, Price = 5.60m, Tag = Jpop },


                new Product { ProductName = "Fast and furios 8", Category = film, Price = 69.69m, Tag = Series, ProductURL= "FastAndFurious8.mp4", Productimage = "CryCry.jpg" },
                new Product { ProductName = "Taken", Category = film, Price = 143.50m, Tag = Series },
                new Product { ProductName = "The big bang theory", Category = film, Price = 69.69m, Tag = Series },
                new Product { ProductName = "Shameless", Category = film, Price = 143.50m, Tag = Series },
                new Product { ProductName = "The walking dead", Category = film, Price = 69.69m, Tag = Series },
                new Product { ProductName = "Stranger things", Category = film, Price = 143.50m, Tag = Series },
                new Product { ProductName = "American gods", Category = film, Price = 69.69m, Tag = Series },
                new Product { ProductName = "Game of Thrones", Category = film, Price = 69.69m, Tag = Series },
                new Product { ProductName = "Pretty little liars", Category = film, Price = 143.50m, Tag = Series },
                new Product { ProductName = "The last kingdom", Category = film, Price = 69.69m, Tag = Series },
                new Product { ProductName = "Blues and Gospel", Category = film, Price = 143.50m, Tag = Series },
                new Product { ProductName = "Sport night", Category = film, Price = 69.69m, Tag = Series },

                new Product { ProductName = "The advengers", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "Max", Category = film, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "Superbad", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "American pie", Category = film, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "Scream", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "The conjuring", Category = film, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "The lord of the rings", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "Harry Potter", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "Titanic", Category = film, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "When Harry met Sally", Category = film, Price = 69.69m, Tag = Movie },
                new Product { ProductName = "Friday night lights", Category = film, Price = 143.50m, Tag = Movie },
                new Product { ProductName = "Rudy", Category = film, Price = 69.69m, Tag = Movie },

                new Product { ProductName = "Scoopy Doo", Category = film, Price = 69.69m, Tag = Cartoon },
                new Product { ProductName = "Kung fu panda", Category = film, Price = 143.50m, Tag = Cartoon },
                new Product { ProductName = "Bad man", Category = film, Price = 69.69m, Tag = Cartoon },
                new Product { ProductName = "Super man", Category = film, Price = 143.50m, Tag = Cartoon },
                new Product { ProductName = "Animated", Category = film, Price = 69.69m, Tag = Cartoon },
                new Product { ProductName = "Mickey mouse school", Category = film, Price = 143.50m, Tag = Cartoon },
                new Product { ProductName = "Aladdin", Category = film, Price = 69.69m, Tag = Cartoon },
                new Product { ProductName = "Fire and Ice", Category = film, Price = 69.69m, Tag = Cartoon },

                new Product { ProductName = "Absolute duo", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "World break", Category = film, Price = 143.50m, Tag = Anime },
                new Product { ProductName = "One Piece", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "Sword art online", Category = film, Price = 143.50m, Tag = Anime },
                new Product { ProductName = "Kiss x sis Oda", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "Black lagoon", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "Highschool DxD", Category = film, Price = 143.50m, Tag = Anime },
                new Product { ProductName = "True Blood", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "Bleach", Category = film, Price = 143.50m, Tag = Anime },
                new Product { ProductName = "Pokemon", Category = film, Price = 69.69m, Tag = Anime },
                new Product { ProductName = "High kyuu", Category = film, Price = 143.50m, Tag = Anime },
                new Product { ProductName = "Inazuma eleven", Category = film, Price = 69.69m, Tag = Anime },

                //Duy's database
                new Product { ProductName = "Attack on Titan", Category = film, Price = 19.50m, Tag = Anime },
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
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Dawn of the apocalypse"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Awakening"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "On raining days"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Wedding dress"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Day By Day"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Cry cry"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "My Country is the best"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Women are Flowers"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Your scent"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bicycle"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "10 Minutes"), TagDetail = RBSoul },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Holding onto the End of the Night"), TagDetail = RBSoul },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "In the end"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Waiting for the end"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "When I was your man"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Baby"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Something just like this"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Closer"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Love story"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fifteen"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Timber"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The monster"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Criminal"), TagDetail = RBSoul },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The day you went away"), TagDetail = RBSoul },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Chỉ là giấc mơ"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Tâm hồn của đá"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Yêu là tha thu"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Đã từng vô giá"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Con bướm xuân"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Let me be your love"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bà tôi"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Làng quan họ quê tôi"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Quăng tao cái boong"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Đưa nhau đi trốn"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bâng khuâng"), TagDetail = RBSoul },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Em đã từng"), TagDetail = RBSoul },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Senbonzakura"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "TOP TEN"), TagDetail = Rock },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Oboduky"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Magnet"), TagDetail = Pop },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Hanahaodori"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Transfer"), TagDetail = Dance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Whisper of the heart"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "A fool such as l"), TagDetail = Country },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "It G ma"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Iruka"), TagDetail = Rap },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Missing"), TagDetail = RBSoul },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "P.P.M"), TagDetail = RBSoul },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fast and furios 8"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Taken"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The big bang theory"), TagDetail = Comedy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Shameless"), TagDetail = Comedy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The walking dead"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Stranger things"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "American Gods"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Game of Thrones"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Pretty little liars"), TagDetail = Romance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The last kingdom"), TagDetail = Romance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Blues and Gospel"), TagDetail = Sport },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Sport night"), TagDetail = Sport },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The advengers"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Max"), TagDetail = Action },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Superbad"), TagDetail = Comedy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "American pie"), TagDetail = Comedy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Scream"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The conjuring"), TagDetail = Horror },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "The lord of the rings"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Harry Potter"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Titanic"), TagDetail = Romance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "When Harry met Sally"), TagDetail = Romance },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Friday night lights"), TagDetail = Sport },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Rudy"), TagDetail = Sport },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Scoopy Doo"), TagDetail = Funny },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Kung fu panda"), TagDetail = Funny },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bad man"), TagDetail = SuperHero },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Super man"), TagDetail = SuperHero },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Animated"), TagDetail = Education },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Mickey mouse school"), TagDetail = Education },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Aladdin"), TagDetail = Fantasy },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Fire and Ice"), TagDetail = Fantasy },

                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Absolute duo"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "World break"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "One Piece"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Sword art online"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Kiss x sis Oda"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Black lagoon"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Highschool DxD"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "True Blood"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Bleach"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Pokemon"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "High kyuu"), TagDetail = Shounen },
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Inazuma eleven"), TagDetail = Shounen },

                //Duy's database
                new TagHelper { Product = context.Products.FirstOrDefault(p => p.ProductName == "Attack on Titan"), TagDetail = Shounen },
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
