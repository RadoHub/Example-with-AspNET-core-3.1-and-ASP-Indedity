using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Abstract
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        List<Category> GetCategoriesByName(string categoryName);
        Category GetCategoryByIdWithProducts(int id);
        List<Category> GetProductCategories(int id);
    }
}
