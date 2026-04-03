using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class UnitTypeController : Controller
    {
        private readonly IUnitTypeService _unittypeService;

        public UnitTypeController(IUnitTypeService unittypeService)
        {
            _unittypeService = unittypeService;
        }

        public IActionResult ViewUnitType()
        {
            try
            {
                var unittypes = _unittypeService.GetUnitTypes();

                return View("~/Views/Product/UnitType.cshtml", unittypes);
            }
            catch
            {
                return View("~/Views/Product/UnitType.cshtml", new List<UnitType>());
            }
        }

        [HttpPost]
        public IActionResult Create(UnitType model)
        {
            if (string.IsNullOrEmpty(model.UnitTypeName))
                TempData["Error"] = "UnitType Name is required.";
            else
                TempData["Success"] = _unittypeService.InsertUnitType(model);

            return RedirectToAction(nameof(ViewUnitType));
        }

        [HttpPost]
        public IActionResult Update(UnitType model)
        {
            TempData["Success"] = _unittypeService.UpdateUnitType(model);
            return RedirectToAction(nameof(ViewUnitType));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            TempData["Success"] = _unittypeService.DeleteUnitType(id);
            return RedirectToAction(nameof(ViewUnitType));
        }

        [HttpPost]
        public IActionResult DeleteSelected([FromForm] int[] ids)
        {
            foreach (var id in ids)
            {
                _unittypeService.DeleteUnitType(id);
            }

            return Ok();
        }
    }
}