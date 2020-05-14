using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Abstract
{
    public interface IProductCategoryService
    {
        public ProductCategory GetProductCategoryByCatIdProdID(int categoryId, int productId);
        public void UpdateProductCategory(ProductCategory productCategoryEntity);
        public void DeleteProductFromCategory(int categoryId, int productId);


    }
}
