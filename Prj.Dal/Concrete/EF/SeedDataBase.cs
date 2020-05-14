using Microsoft.EntityFrameworkCore;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public static class SeedDataBase
    {
        private static Product[] Products = {
            new Product() { Name = "Samsung s5", ImgUrl = "galaxys5.png", Price = 2000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "Samsung s6", ImgUrl = "galaxys5.png", Price = 3000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "Samsung s7", ImgUrl = "galaxys5.png", Price = 4000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "Samsung s8", ImgUrl = "galaxys5.png", Price = 5000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "Samsung s9", ImgUrl = "galaxys5.png", Price = 6000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "IPhone 6", ImgUrl = "iphone.png", Price = 2000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name = "IPhone 7", ImgUrl = "iphone.png", Price = 5000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Razor Desktop", ImgUrl="desktop.png", Price=4000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Acer Aspire 3", ImgUrl="laptop.png", Price=3500, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="MSI Alien", ImgUrl="laptop.png", Price=4500, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Lenovo Desktop", ImgUrl="desktop.png", Price=4000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="MacBook A1", ImgUrl="macbook.png", Price=5000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="MacBook Pro", ImgUrl="macbook.png", Price=6000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Apple Tablet 3", ImgUrl="macbook.png", Price=4000, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Apple Tablet Pro", ImgUrl="macbook.png", Price=5500, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"},
            new Product() { Name="Galaxy Tab 3", ImgUrl="galaxytab.png", Price=5500, Description="<p>Donec vel mattis nisl, in tristique lacus.</p>"}
        };
        private static Category[] Categories = {
            new Category(){Name="Desktop PC"},
            new Category(){Name="Laptop"},
            new Category(){Name="Smart Phones"},
            new Category(){Name="Tablets"},
            new Category(){Name="Electronic"}
         };
        private static ProductCategory[] ProductCategories = {
           new ProductCategory() {Product=Products[0], Category=Categories[2]},
           new ProductCategory() {Product=Products[0], Category=Categories[4]},
           new ProductCategory() {Product=Products[1], Category=Categories[2]},
           new ProductCategory() {Product=Products[1], Category=Categories[4]},
           new ProductCategory() {Product=Products[2], Category=Categories[2]},
           new ProductCategory() {Product=Products[2], Category=Categories[4]},
           new ProductCategory() {Product=Products[3], Category=Categories[2]},
           new ProductCategory() {Product=Products[3], Category=Categories[4]},
           new ProductCategory() {Product=Products[4], Category=Categories[2]},
           new ProductCategory() {Product=Products[4], Category=Categories[4]},
           new ProductCategory() {Product=Products[5], Category=Categories[2]},
           new ProductCategory() {Product=Products[5], Category=Categories[4]},
           new ProductCategory() {Product=Products[6], Category=Categories[2]},
           new ProductCategory() {Product=Products[6], Category=Categories[4]},
           new ProductCategory() {Product=Products[7], Category=Categories[0]},
           new ProductCategory() {Product=Products[7], Category=Categories[4]},
           new ProductCategory() {Product=Products[8], Category=Categories[1]},
           new ProductCategory() {Product=Products[8], Category=Categories[4]},
           new ProductCategory() {Product=Products[9], Category=Categories[1]},
           new ProductCategory() {Product=Products[9], Category=Categories[4]},
           new ProductCategory() {Product=Products[10], Category=Categories[0]},
           new ProductCategory() {Product=Products[10], Category=Categories[4]},
           new ProductCategory() {Product=Products[11], Category=Categories[1]},
           new ProductCategory() {Product=Products[11], Category=Categories[4]},
           new ProductCategory() {Product=Products[12], Category=Categories[1]},
           new ProductCategory() {Product=Products[12], Category=Categories[4]},
           new ProductCategory() {Product=Products[13], Category=Categories[3]},
           new ProductCategory() {Product=Products[13], Category=Categories[4]},
           new ProductCategory() {Product=Products[14], Category=Categories[3]},
           new ProductCategory() {Product=Products[14], Category=Categories[4]},
           new ProductCategory() {Product=Products[15], Category=Categories[3]},
           new ProductCategory() {Product=Products[15], Category=Categories[4]}
        };

        public static void SeedData()
        {
            ShopContext con = new ShopContext();
            if (con.Database.GetPendingMigrations().Count() == 0)
            {
                if (con.Categories.Count() == 0)
                {
                    con.Categories.AddRange(Categories);
                }
                if (con.Products.Count() == 0)
                {
                    con.Products.AddRange(Products);
                }
                if (con.ProductsCategories.Count() == 0)
                {
                    con.ProductsCategories.AddRange(ProductCategories);
                }
            }
            con.SaveChanges();
        }

    }
}
