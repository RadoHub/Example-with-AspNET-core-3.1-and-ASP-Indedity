using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prj.Bus.Abstract;
using Prj.Ent.Concrete;
using Prj.WebUI.Models;

namespace Prj.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryService _productCategoryService;
        public AdminController(IProductService productService, ICategoryService categoryService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
        }

        public IActionResult ProductList(int page = 1)
        {
            int pageSize = 5;
            var entites = _productService.GetAllProducts().Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var count = _productService.GetAllProducts().Count();

            return View(new ProductsModel { Products = entites, PageModel = new PageViewModel { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = count } });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel productModel, IFormFile file )
        {
            if (_productService.Validate(productModel) && ModelState.IsValid)
            {
                if (file != null)
                {
                    var entity = new Product() { Name = productModel.Name, Price = productModel.Price, Description = productModel.Description,
                        ImgUrl = DateTime.Now.ToString("yyyyMMddHHss") + file.FileName
                };
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", entity.ImgUrl);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _productService.CreateProduct(entity);
                    return RedirectToAction("ProductList");
                }
                else 
                {
                    var entity2 = new Product() { ImgUrl = productModel.ImgUrl, Name = productModel.Name, Price = productModel.Price, Description = productModel.Description };

                    _productService.CreateProduct(entity2);
                    return RedirectToAction("ProductList");
                }
            }

            else
            {
                ViewBag.Errors = _productService.ErrorMessage;
                return View(productModel);
            }


            // Alternatif yolu ... 
            //_productService.CreateProduct(productModel);
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (!id.HasValue)
            {
                return NoContent();
            }
            var entity = _productService.GetProductByIdWithCategory((int)id);
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgUrl = entity.ImgUrl,
                Price = entity.Price,
                SelectedCategories = entity.ProductCategory.Select(x => x.Category).ToList()
            };
            ViewBag.AllCategories = _categoryService.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)  
        {
            if (ModelState.IsValid && _productService.Validate(model))
            {
                var entity = _productService.GetProductById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                if (file != null)
                {                   
                    var deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", entity.ImgUrl==null? "" : entity.ImgUrl.ToString());
                    entity.ImgUrl =  DateTime.Now.ToString("yyyyMMddHHss") + file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", entity.ImgUrl);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        if (System.IO.File.Exists(deletepath))
                        {
                            System.IO.File.Delete(deletepath);
                        }
                    }
                } 
                _productService.UpdateProduct(entity, categoryIds);
                return RedirectToAction("ProductList");
            }
            else
            {
                ViewBag.AllCategories = _categoryService.GetAllCategories();
                ViewBag.Errors = _productService.ErrorMessage;
                return View(model);

                #region not

                #endregion

            }

        }

        public IActionResult DeleteProduct(int? id)
        {
            if (!id.HasValue)
            {
                return NoContent();
            }
            var entity = _productService.GetProductById((int)id);
            _productService.DeleteProduct(entity);
            return RedirectToAction("ProductList");
        }

        public IActionResult CategoryList(int page = 1)
        {
            int pageSize = 5;
            var entities = _categoryService.GetAllCategories().Skip((page - 1) * pageSize).Take(pageSize).ToList(); ;
            var count = _categoryService.GetAllCategories().Count();
            return View(new CategoryListModel { Categories = entities, PageInfo = new PageViewModel { TotalItems = count, CurrentPage = page, ItemsPerPage = pageSize } });
        }

        [HttpGet]
        public IActionResult EditCategory(int categoryId, int page = 1)
        {
            int pageSize = 4;
            var entity = _categoryService.GetCategoryByIdWithProducts(categoryId);

            if (entity == null)
            {
                return NoContent();
            }


            return View(new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategory.Select(x => x.Product).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageInfo = new PageViewModel { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = entity.ProductCategory.Select(x => x.Product).ToList().Count(), CurrentObj = categoryId.ToString() }

            });
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetCategoryById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.UpdateCategory(entity);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new CategoryModel()); 
        }

        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category { Name = model.Name };
                _categoryService.CreateCategory(entity);
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetCategoryById(categoryId);
            _categoryService.DeleteCategory(entity);
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategoryProduct(int categoryId, int productId)
        {
            _productCategoryService.DeleteProductFromCategory(categoryId, productId);
            return RedirectToAction("EditCategory", "Admin", new { categoryId });
        }
    }
}



