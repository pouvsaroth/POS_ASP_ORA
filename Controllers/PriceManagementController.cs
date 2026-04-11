using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class PriceManagementController : Controller
    {
        private readonly IPriceManagementService _priceManagementService;

        public PriceManagementController(IPriceManagementService productPriceService)
        {
            _priceManagementService = productPriceService;
        }

        // ✅ LOAD VIEW
        public IActionResult ViewPriceManagement()
        {
            var list = _priceManagementService.GetProductPrices();
            return View("~/Views/Purchase/PriceManagement.cshtml", list);
        }

        // ✅ INSERT
        [HttpPost]
        public IActionResult Create(PriceManagementModel model)
        {
            if (model.ProductId == 0 || model.SalePrice <= 0)
            {
                TempData["Error"] = "Product and Sale Price are required.";
                return RedirectToAction("ViewPriceManagement");
            }

            string message = _priceManagementService.InsertProductPrice(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewPriceManagement");
        }

        // ✅ UPDATE
        [HttpPost]
        public IActionResult Update(PriceManagementModel model)
        {
            if (model.ProductId == 0 || model.SalePrice <= 0)
            {
                TempData["Error"] = "Product and Sale Price are required.";
                return RedirectToAction("ViewPriceManagement");
            }

            string message = _priceManagementService.UpdateProductPrice(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewPriceManagement");
        }

        // ✅ DELETE SINGLE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _priceManagementService.DeleteProductPrice(id);
            return Json(new { message = result });
        }

        // ✅ DELETE MULTIPLE
        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No records selected for deletion." });
            }

            foreach (var id in ids)
            {
                _priceManagementService.DeleteProductPrice(id);
            }

            return Json(new { message = "Selected records deleted successfully." });
        }
    }
}
