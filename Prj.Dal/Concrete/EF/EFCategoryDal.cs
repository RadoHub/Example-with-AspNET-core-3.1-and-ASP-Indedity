using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Prj.Dal.Concrete.EF
{
    public class EFCategoryDal : EFBaseRepository<Category, ShopContext>, ICategoryDal
    {
        public List<Category> GetCategoriesByName(string categoryName)
        {
            using ShopContext con = new ShopContext();
            return con.Set<Category>().Where(x => x.Name == categoryName).ToList();
        }

        public Category GetCategoryByIdWithProducts(int id)
        {
            using var con = new ShopContext();
            return con.Categories.Where(x => x.Id == id).Include(x => x.ProductCategory).ThenInclude(x => x.Product).FirstOrDefault();
        }

        public List<Category> GetProductCategories(int id)
        {
            using (ShopContext con = new ShopContext())
            {
                return con.Set<Category>().Include(x => x.ProductCategory).ThenInclude(x => x.Product).Where(x => x.ProductCategory.Any(x => x.ProductId == id)).ToList();
            }
        }
    }
}


