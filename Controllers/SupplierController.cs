using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // VIEW PAGE
        public IActionResult ViewSupplier()
        {
            try
            {
                var suppliers = _supplierService.GetSuppliers();
                return View("~/Views/Product/Supplier.cshtml", suppliers);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load suppliers: " + ex.Message;
                return View("~/Views/Product/Supplier.cshtml", new List<Supplier>());
            }
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(Supplier model)
        {
            var message = _supplierService.InsertSupplier(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewSupplier");
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(Supplier model)
        {
            if (string.IsNullOrEmpty(model.SupplierName))
            {
                TempData["Error"] = "Supplier Name is required.";
                return RedirectToAction("ViewSupplier");
            }

            var message = _supplierService.UpdateSupplier(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewSupplier");
        }

        // DELETE SINGLE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _supplierService.DeleteSupplier(id);
            return Json(new { message = result });
        }

        // DELETE MULTIPLE
        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No suppliers selected for deletion." });
            }

            foreach (var id in ids)
            {
                _supplierService.DeleteSupplier(id);
            }

            return Json(new { message = "Selected suppliers deleted successfully." });
        }
    }
}