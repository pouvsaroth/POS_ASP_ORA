using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        // LIST
        public IActionResult ViewCurrency()
        {
            try
            {
                var list = _currencyService.GetCurrencies();

                return View("~/Views/Setting/Currency.cshtml", list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load currency: " + ex.Message;

                return View("~/Views/Setting/Currency.cshtml", new List<CurrencyModel>());
            }
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(CurrencyModel model)
        {
            if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name))
            {
                TempData["Error"] = "Currency Code and Name are required.";
                return RedirectToAction(nameof(ViewCurrency));
            }

            string message = _currencyService.InsertCurrency(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction(nameof(ViewCurrency));
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(CurrencyModel model)
        {
            if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Name))
            {
                TempData["Error"] = "Currency Code and Name are required.";
                return RedirectToAction(nameof(ViewCurrency));
            }

            string message = _currencyService.UpdateCurrency(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction(nameof(ViewCurrency));
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _currencyService.DeleteCurrency(id);
            return Json(new { message = result });
        }

        // DELETE MULTIPLE
        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No currency selected." });
            }

            foreach (var id in ids)
            {
                _currencyService.DeleteCurrency(id);
            }

            return Json(new { message = "Selected currency deleted successfully." });
        }
    }
}