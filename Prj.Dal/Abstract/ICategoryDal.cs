using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface ICategoryDal: IRepository<Category>
    {
        List<Category> GetCategoriesByName(string categoryName);
        List<Category> GetProductCategories(int id);
        Category GetCategoryByIdWithProducts(int id);

    }
}
