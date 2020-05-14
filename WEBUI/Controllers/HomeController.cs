using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prj.Bus.Abstract;
using Prj.WebUI.Models;

namespace Prj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var entites = _productService.GetAllProducts();
            return View(new ProductsModel() { Products = entites }) ;
        }
        
    }
}