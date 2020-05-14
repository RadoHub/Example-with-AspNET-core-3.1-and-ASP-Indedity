using Prj.Bus.Abstract;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Bus.Concrete
{
    public class ProductManager : IProductService
    {
        private  readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public int CategoryProductsCountByCategoryId(int categoryId)
        {
            return _productDal.CategoryProductsCountByCategoryId(categoryId);
        }

        public bool CreateProduct(Product product)
        {
            if (Validate(product))
            {
                _productDal.CreateObj(product);
                return true;
            }            
            return false;
        }

        public void DeleteProduct(Product product)
        {
            _productDal.DeleteObj(product);
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAllObjs().ToList();
        }

        public Product GetOneProduct()
        {
            return _productDal.GetOneObj();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetObjById(id);
        }

        public Product GetProductByIdWithCategory(int id)
        {
            return _productDal.GetProductByIdWithCategory(id);
        }

        public List<Product> GetProductsByCategory(int id, int page, int pageSize)
        {
            return _productDal.GetProductsByCategory(id, page, pageSize);
        }

        public void UpdateProduct(Product product)
        {
            _productDal.UpdateObj(product);
        }

        public void UpdateProduct(Product model, int[] categoryIds)
        {
            _productDal.UpdateProduct(model, categoryIds);
        }
        public string ErrorMessage { get; set; }

        public bool Validate(Product entity)
        {
            var isValid = true;
            if (String.IsNullOrEmpty(entity.Name) || entity.Name.Length<3 || entity.Name.Length>20)
            {
                ErrorMessage += "Product name must be entered and must be between 3 and 20 characters";
                isValid = false;
            }

            if (String.IsNullOrEmpty(entity.Description) || entity.Description.Length<5 || entity.Description.Length > 200)
            {
                ErrorMessage += "Product description must be entered and must be between 5 and 200 characters";
                isValid = false;
            }
            return isValid;
        }
    }
}
