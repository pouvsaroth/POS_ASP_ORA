using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class IncomeTypeController : Controller
    {
        private readonly IIncomeTypeService _incometypeService;

        public IncomeTypeController(IIncomeTypeService incomeTypeService)
        {
            _incometypeService = incomeTypeService;
        }

        public IActionResult ViewIncomeType()
        {
            try
            {
                var incometypes = _incometypeService.GetIncomeTypes();
                return View("~/Views/Finance/IncomeType.cshtml", incometypes);
            }
            catch
            {
                return View("~/Views/Finance/IncomeType.cshtml", new List<IncomeType>());
            }
        }

        [HttpPost]
        public IActionResult Create(IncomeType model)
        {
            if (string.IsNullOrEmpty(model.TypeName))
                TempData["Error"] = "IncomeType Name is required.";
            else
                TempData["Success"] = _incometypeService.InsertIncomeType(model);

            return RedirectToAction(nameof(ViewIncomeType));
        }

        [HttpPost]
        public IActionResult Update(IncomeType model)
        {
            TempData["Success"] = _incometypeService.UpdateIncomeType(model);
            return RedirectToAction(nameof(ViewIncomeType));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            TempData["Success"] = _incometypeService.DeleteIncomeType(id);
            return RedirectToAction(nameof(ViewIncomeType));
        }

        [HttpPost]
        public IActionResult DeleteSelected([FromForm] int[] ids)
        {
            foreach (var id in ids)
                _incometypeService.DeleteIncomeType(id);

            TempData["Success"] = "Selected IncomeTypes deleted successfully.";
            return RedirectToAction(nameof(ViewIncomeType));
        }
    }
}