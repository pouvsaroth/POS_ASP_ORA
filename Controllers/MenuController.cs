using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Controllers
{
    public class MenuController:Controller
    {
        private readonly MenuService _service;

        public MenuController(MenuService service)
        {
            _service = service;
        }

        public IActionResult ViewMenu()
        {
            var menus = _service.GetMenus();
            return View("~/Views/Settings/Menu.cshtml",menus);
        }

        [HttpPost]
        public IActionResult Create(MenuModel model)
        {
            var msg = _service.Insert(model);
            TempData["Message"] = msg;
            return RedirectToAction("ViewMenu");
        }

        [HttpPost]
        public IActionResult Update(MenuModel model)
        {
            var msg = _service.Update(model);
            TempData["Message"] = msg;
            return RedirectToAction("ViewMenu");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(new { message = _service.Delete(id) });
        }
    }
}
