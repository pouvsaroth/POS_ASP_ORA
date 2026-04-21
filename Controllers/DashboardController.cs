using Microsoft.AspNetCore.Mvc;

namespace POS_ASP_ORA.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult ViewDashboard()
        {
            ViewBag.TodaySales = 120;
            ViewBag.TotalOrders = 25;
            ViewBag.TotalProducts = 120;
            ViewBag.LowStock = 5;

            ViewBag.SalesData = new int[] { 50, 80, 60, 120, 150, 130, 170 };

            ViewBag.RecentOrders = new List<dynamic>
            {
                new { InvoiceNo = "INV001", Date = "2026-04-21", Total = 10 },
                new { InvoiceNo = "INV002", Date = "2026-04-21", Total = 20 }
            };

            ViewBag.TopProducts = new List<dynamic>
            {
                new { ProductName = "Milk Coffee", Qty = 50 },
                new { ProductName = "Green Tea", Qty = 40 }
            };

            return View("~/Views/Dashboard.cshtml");
        }
    }
}
