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

            return Json(result);
        }
    }
}
