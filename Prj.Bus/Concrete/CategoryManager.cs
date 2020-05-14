using Prj.Bus.Abstract;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Bus.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void CreateCategory(Category category)
        {
            _categoryDal.CreateObj(category);
        }

        public void DeleteCategory(Category category)
        {
            _categoryDal.DeleteObj(category);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAllObjs().ToList();
        }

        public List<Category> GetCategoriesByName(string categoryName)
        {
            return _categoryDal.GetCategoriesByName(categoryName).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryDal.GetObjById(id);
        }

        public Category GetCategoryByIdWithProducts(int id)
        {
            return _categoryDal.GetCategoryByIdWithProducts(id);
        }

    
        public List<Category> GetProductCategories(int id)
        {
            return _categoryDal.GetProductCategories(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDal.UpdateObj(category);
        }
    }
}
