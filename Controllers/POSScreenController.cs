using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class POSScreenController:Controller
    {
        private readonly IProductCategoryService _categoryService;
        private readonly IProductListService _productListService;
        private readonly IConfiguration _config;
        public POSScreenController(IProductCategoryService categoryService, IProductListService productListService, IConfiguration config)
        {
            _categoryService = categoryService;
            _productListService = productListService;
            _config = config;
        }

        // LIST
        public IActionResult ViewPOSScreen()
        {
            var model = new POSScreenModel
            {
                Products = _productListService.GetProducts(),
                Categories = _categoryService.GetCategories(),
                ImageBasePath = _config["FileUpload:ProductImagePath"]
            };
            
            return View("~/Views/Sales/POSScreen.cshtml",model);
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (string.IsNullOrEmpty(model.CategoryName))
            {
                TempData["Error"] = "Category Name is required.";
                return RedirectToAction("ProductCategory");
            }

            string message = _categoryService.InsertCategory(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction("ProductCategory");
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(Category model)
        {
            if (string.IsNullOrEmpty(model.CategoryName))
            {
                TempData["Error"] = "Category Name is required.";
                return RedirectToAction("ProductCategory");
            }
            string message = _categoryService.UpdateCategory(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction("ProductCategory");
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return Json(new { message = result });
        }

        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No categories selected for deletion." });
            }

            foreach (var id in ids)
            {
                _categoryService.DeleteCategory(id);
            }

            return Json(new { message = "Selected categories deleted successfully." });
        }
    }
}
