using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services;
using POS_ASP_ORA.Services.Interfaces;
namespace POS_ASP_ORA.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseService;

        public PurchaseOrderController(IPurchaseOrderService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        public IActionResult ViewPurchaseOrder()
        {
            var data = _purchaseService.GetPurchases();
            return View("~/Views/Purchase/PurchaseOrder.cshtml", data);
        }
        [HttpPost]
        public IActionResult Save([FromBody] PurchaseModel model)
        {
            if (model == null || model.Items == null || model.Items.Count == 0)
                return Json("Invalid data");

            var result = _purchaseService.InsertPurchase(model);
            //payment
            if (result.id > 0) {
                _purchaseService.InsertPayment(result.id, model.Paid);
            }
            //detail
            if (result.id > 0 && model.Items.Count>0) {
                foreach (var detail in model.Items)
                {
                    detail.PurchaseId = result.id;
                    _purchaseService.InsertDetail(detail);
                    
                }
            }

            return Json(result);
        }

        // =========================
        // DELETE MULTIPLE (AJAX)
        // =========================
        [HttpPost]
        public IActionResult DeleteMultiple(List<int> ids)
        {


            if (ids == null || ids.Count == 0)
                return Json("No items selected");

            var result = _purchaseService.DeleteMultiple(ids);

            return Json(result);
        }
    }
}
