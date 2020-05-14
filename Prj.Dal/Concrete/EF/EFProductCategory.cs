using Microsoft.EntityFrameworkCore;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class EFProductCategory : EFBaseRepository<ProductCategory, ShopContext>, IProductCategoryDal
    {
        public void DeleteCategeryFromProduct(int categoryId, int productId)
        {
            using var context = new ShopContext();
            var sqlcommand = @"Delete from ProductsCategories WHERE CategoryId=@p0 AND ProductId=@p1";
            context.Database.ExecuteSqlCommand(sqlcommand, categoryId, productId);
        }

        public ProductCategory GetProductCategoryByCatIdProdID(int categoryId, int productId)  
        {
            using var con = new ShopContext();
           return con.ProductsCategories.Include(x => x.Product).Include(x => x.Category).FirstOrDefault(x => x.CategoryId == categoryId && x.ProductId == productId);  

        }

    }
}
