using Microsoft.EntityFrameworkCore;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class ShopContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=cleanPrjDB;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.CategoryId, x.ProductId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cart> Carts { get; set; }

        //CartItem i istersek ekleyebiliriz, lakin bunu DBSET olarak eklemeden direkt Cart uzerinden de erisebilecegimizden DBSET e koymasakda olur
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
