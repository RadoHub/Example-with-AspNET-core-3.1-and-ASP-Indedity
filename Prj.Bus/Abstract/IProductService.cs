using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        bool CreateProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetOneProduct();
        public List<Product> GetProductsByCategory(int id, int page, int pageSize);
        public int CategoryProductsCountByCategoryId(int categoryId);
        Product GetProductByIdWithCategory(int id);
        void UpdateProduct(Product model, int[] categoryIds);
    }
}
