using Microsoft.EntityFrameworkCore;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class EFProductDal : EFBaseRepository<Product, ShopContext>, IProductDal
    {
        public int CategoryProductsCountByCategoryId(int categoryId)
        {
            using ShopContext con = new ShopContext();
            return con.Set<Product>().Include(x => x.ProductCategory).ThenInclude(x => x.Category).Where(x => x.ProductCategory.Any(x => x.CategoryId == categoryId)).Count();
        }

        public Product GetProductByIdWithCategory(int id)
        {
            using var con = new ShopContext();
            return con.Products.Include(x => x.ProductCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProductsByCategory(int id, int page, int pageSize)
        {
            using ShopContext con = new ShopContext();
            return con.Set<Product>().Include(x => x.ProductCategory).ThenInclude(x => x.Category).Where(x => x.ProductCategory.Any(x => x.CategoryId == id)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void UpdateProduct(Product model, int[] categoryIds)
        {
            using var con = new ShopContext();
            var product = con.Products.Include(x => x.ProductCategory).FirstOrDefault(x => x.Id == model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.ImgUrl = model.ImgUrl;
                product.Price =  model.Price;
                product.Description = model.Description;
                product.ProductCategory = categoryIds.Select(catId => new ProductCategory
                {
                 CategoryId = catId,
                 ProductId = model.Id
                }).ToList();
            }
            con.SaveChanges();
        }
    }
}
