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

          

        }
    }
}
