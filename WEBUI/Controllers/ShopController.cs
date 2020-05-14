using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj.Bus.Abstract;
using Prj.Ent.Concrete;
using Prj.WebUI.Models;

namespace Prj.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int? Id, int page=1)
        {
            int pageSize = 4;
            if (!Id.HasValue)
            {
                var entityCount = _productService.GetAllProducts().Count();
                return View(new ProductsModel() { Products = _productService.GetAllProducts().Skip((page - 1) * pageSize).Take(pageSize).ToList() , PageModel= new PageViewModel { CurrentPage=page, ItemsPerPage = pageSize , TotalItems=entityCount} }); ;
            }


            
            var count = _productService.CategoryProductsCountByCategoryId((int)Id);
            return View(new ProductsModel() { Products = _productService.GetProductsByCategory((int)Id, page, pageSize), PageModel = new PageViewModel { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = count } });
            }



        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return NoContent();
            }

            return View(new ProductDetailsModel()
            {
                Product = _productService.GetProductById((int)id),
                Categories = _categoryService.GetProductCategories((int)id)
            }); ;
        }
    }
}