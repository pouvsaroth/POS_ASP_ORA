using Microsoft.AspNetCore.Mvc;

namespace POS_ASP_ORA.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
