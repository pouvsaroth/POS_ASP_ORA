using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class RightController : Controller
    {
        private readonly IRightService _service;

        public RightController(IRightService service)
        {
            _service = service;
        }

        // MAIN SCREEN
        public IActionResult ViewRight()
        {
            var groups = _service.GetGroups();
            return View("~/Views/Settings/Right.cshtml", groups);
        }

        // AJAX: LOAD MENU
        public IActionResult GetMenus(int groupId)
        {
            var menus = _service.GetMenusByGroup(groupId);
            return Json(menus);
        }

        // SAVE
        [HttpPost]
        public IActionResult Save(int groupId, [FromBody] List<int> menuIds)
        {
            var result = _service.SaveGroupMenu(groupId, menuIds);
            return Json(new { message = result });
        }
    }
}