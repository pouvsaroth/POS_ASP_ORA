using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // LOAD DATA
        public IActionResult ViewCompany()
        {
            var companylist = _companyService.GetCompanies();
            return View("~/Views/Settings/Company.cshtml", companylist);
        }

        // INSERT
        [HttpPost]
        public IActionResult Create(Company model)
        {
            string message = _companyService.InsertCompany(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewCompany");
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(Company model)
        {
            if (string.IsNullOrEmpty(model.CompanyName))
            {
                TempData["Error"] = "Company Name is required.";
                return RedirectToAction("Company");
            }
            string message = _companyService.UpdateCompany(model);
            TempData["Success"] = message;

            return RedirectToAction("ViewCompany");
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _companyService.DeleteCompany(id);
            return Json(new { message = result });
        }

        [HttpPost]
        public IActionResult DeleteSelected([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No categories selected for deletion." });
            }

            foreach (var id in ids)
            {
                _companyService.DeleteCompany(id);
            }

            return Json(new { message = "Selected categories deleted successfully." });
        }
    }
}