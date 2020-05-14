using Prj.Bus.Abstract;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Concrete
{
    public class ProductCategoryManager: IProductCategoryService
    {
        private readonly IProductCategoryDal _productCategoryDal;
        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public void DeleteProductFromCategory(int categoryId, int productId)
        {
            _productCategoryDal.DeleteCategeryFromProduct(categoryId, productId);
        }

        public ProductCategory GetProductCategoryByCatIdProdID(int categoryId, int productId)
        {
            return _productCategoryDal.GetProductCategoryByCatIdProdID(categoryId, productId);
        }

        public void UpdateProductCategory(ProductCategory productCategoryEntity)
        {
            _productCategoryDal.UpdateObj(productCategoryEntity);
        }
    }
}
