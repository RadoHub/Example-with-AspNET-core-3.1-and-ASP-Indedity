using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface IProductCategoryDal: IRepository<ProductCategory>
    {
        public ProductCategory GetProductCategoryByCatIdProdID(int categoryId, int productId); 

        public void DeleteCategeryFromProduct(int categoryId, int productId);

    }
}
