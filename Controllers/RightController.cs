using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
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
        public IActionResult Save([FromBody] SaveRightModel model)
        {
            var result = _service.SaveGroupMenu(model.GroupId, model.MenuIds);
            return Json(new { message = result });
        }

        [HttpPost]
        public IActionResult CreateGroup([FromBody] GroupModel model)
        {
            var result = _service.CreateGroup(model);
            return Json(new { message = result });
        }

        [HttpPost]
        public IActionResult UpdateGroup([FromBody] GroupModel model)
        {
            var result = _service.UpdateGroup(model);
            return Json(new { message = result });
        }
    }
}