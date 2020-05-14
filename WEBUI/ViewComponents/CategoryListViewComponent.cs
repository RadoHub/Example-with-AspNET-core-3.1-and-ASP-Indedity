using Microsoft.AspNetCore.Mvc;
using Prj.Bus.Abstract;
using Prj.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(new CategoriesMenuModels()
            {
                Categories = _categoryService.GetAllCategories(),
                CurrentCategoryId = Convert.ToInt32(RouteData.Values["id"]?.ToString()) 
            }) ;
        }
    }
}
