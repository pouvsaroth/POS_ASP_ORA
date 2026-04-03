using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class ProductUnitController : Controller
    {
        private readonly IProductUnitService _productUnitService;
        private readonly IUnitTypeService _unittypeService;

        public ProductUnitController(IProductUnitService productUnitService, IUnitTypeService unittypeService)
        {
            _productUnitService = productUnitService;
            _unittypeService = unittypeService;
        }

        // LOAD DATA
        public IActionResult ViewProductUnit()
        {
            var list = _productUnitService.GetProductUnits();
            return View("~/Views/Product/ProductUnit.cshtml", list);
        }

        public IActionResult GetUnitTypes()
        {
            var unitTypes = _unittypeService.GetUnitTypes();
            var result = unitTypes.Select(c => new
            {
                id = c.Id,
                unitTypeName=c.UnitTypeName

            });
            return Json(result);
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(ProductUnit model)
        {
            string message = _productUnitService.InsertProductUnit(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewProductUnit");
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(ProductUnit model)
        {
            if (string.IsNullOrEmpty(model.UnitName))
            {
                TempData["Error"] = "Unit Name is required.";
                return RedirectToAction("ViewProductUnit");
            }

            string message = _productUnitService.UpdateProductUnit(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewProductUnit");
        }

        // DELETE SINGLE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _productUnitService.DeleteProductUnit(id);
            return Json(new { message = result });
        }

        // DELETE MULTIPLE
        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No units selected for deletion." });
            }

            foreach (var id in ids)
            {
                _productUnitService.DeleteProductUnit(id);
            }

            return Json(new { message = "Selected units deleted successfully." });
        }
    }
}