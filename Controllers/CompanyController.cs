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

        // CREATE
        [HttpPost]
        public IActionResult Create(Company model)
        {
            string message = _companyService.InsertCompany(model);
            TempData["Success"] = message;

            return RedirectToAction("Company");
        }

        // UPDATE
        [HttpPost]
        public IActionResult Update(Company model)
        {
            string message = _companyService.UpdateCompany(model);
            TempData["Success"] = message;

            return RedirectToAction("Company");
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _companyService.DeleteCompany(id);
            return Json(new { message = result });
        }
    }
}