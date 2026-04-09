using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class CurrencyRateController : Controller
    {
        private readonly ICurrencyRateService _rateService;
        private readonly ICurrencyService _currencyService;

        public CurrencyRateController(
            ICurrencyRateService rateService,
            ICurrencyService currencyService)
        {
            _rateService = rateService;
            _currencyService = currencyService;
        }

        // LIST
        public IActionResult ViewCurrencyRate()
        {
            try
            {
                var rates = _rateService.GetRates();
                ViewBag.Currencies = _currencyService.GetCurrencies();

                return View("~/Views/Setting/CurrencyRate.cshtml", rates);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load exchange rates: " + ex.Message;

                return View("~/Views/Setting/CurrencyRate.cshtml", new List<CurrencyRateModel>());
            }
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(CurrencyRateModel model)
        {
            if (model.FromCurrency == 0 || model.ToCurrency == 0 || model.Rate <= 0)
            {
                TempData["Error"] = "Invalid currency rate data.";
                return RedirectToAction(nameof(ViewCurrencyRate));
            }

            string message = _rateService.InsertRate(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction(nameof(ViewCurrencyRate));
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(CurrencyRateModel model)
        {
            if (model.FromCurrency == 0 || model.ToCurrency == 0 || model.Rate <= 0)
            {
                TempData["Error"] = "Invalid currency rate data.";
                return RedirectToAction(nameof(ViewCurrencyRate));
            }

            string message = _rateService.UpdateRate(model);

            if (message.Contains("successfully"))
                TempData["Success"] = message;
            else
                TempData["Error"] = message;

            return RedirectToAction(nameof(ViewCurrencyRate));
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _rateService.DeleteRate(id);
            return Json(new { message = result });
        }

        // DELETE MULTIPLE
        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No rate selected." });
            }

            foreach (var id in ids)
            {
                _rateService.DeleteRate(id);
            }

            return Json(new { message = "Selected rates deleted successfully." });
        }
    }
}