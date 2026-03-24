using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class POSScreenController:Controller
    {
        private readonly IProductCategoryService _categoryService;

        public POSScreenController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // LIST
        public IActionResult ViewPOSScreen()
        {
            var model = new POSScreenModel
            {
                Products = new List<ProductTest>
                {
                    new ProductTest { Name = "Coca Cola", Price = 1, Stock = 120, ImageUrl="/images/coke.png" },
                    new ProductTest { Name = "Pepsi", Price = 1, Stock = 80, ImageUrl="/images/pepsi.png" },
                    new ProductTest { Name = "Red Bull", Price = 2, Stock = 50, ImageUrl="/images/redbull.png" }
                } 
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
