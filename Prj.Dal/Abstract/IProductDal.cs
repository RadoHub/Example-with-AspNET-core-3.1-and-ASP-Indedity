using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface IProductDal: IRepository<Product>
    {
        List<Product> GetProductsByCategory(int id, int page, int pageSize);
        int CategoryProductsCountByCategoryId(int categoryId);
        Product GetProductByIdWithCategory(int id);
        void UpdateProduct(Product model, int[] categoryIds);
    }
}
