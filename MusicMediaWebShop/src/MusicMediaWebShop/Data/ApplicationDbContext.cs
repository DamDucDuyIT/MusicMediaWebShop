using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMediaWebShop.Models;

namespace MusicMediaWebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<ShippingInfo>().ToTable("ShippingInfos");
            builder.Entity<OrderDetail>().ToTable("OrderDetail");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<ShippingInfo>().ToTable("Purchase");
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShippingInfo> ShippingInfos { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories{ get; set; }

    }
}
