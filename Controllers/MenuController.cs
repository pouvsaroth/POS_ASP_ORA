using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services.Interfaces;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Controllers
{
    public class MenuController:Controller
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
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

        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No categories selected for deletion." });
            }

            foreach (var id in ids)
            {
                _service.Delete(id);
            }

            return Json(new { message = "Selected categories deleted successfully." });
        }
    }
}
