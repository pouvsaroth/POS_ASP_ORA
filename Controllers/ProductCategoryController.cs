using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Data;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services;

namespace POS_ASP_ORA.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryService _categoryService;

        public ProductCategoryController(ProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // LIST
        public IActionResult ProductCategory()
        {
            try
            {
                var categories = _categoryService.GetCategories();

                return View("~/Views/Product/ProductCategory.cshtml", categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load categories: " + ex.Message;

                return View("~/Views/Product/ProductCategory.cshtml", new List<Category>());
            }
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
