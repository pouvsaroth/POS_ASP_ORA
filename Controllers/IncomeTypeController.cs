using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Data;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Controllers
{
    public class IncomeTypeController : Controller
    {
        private readonly AppDbContext _context;

        public IncomeTypeController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult IncomeType()
        {
            var incometypeData = _context.IncomeTypeModel.ToList();
            return View("~/Views/Accounts/IncomeType.cshtml", incometypeData);
        }

        [HttpPost]
        public IActionResult Create(IncomeType model)
        {
            if (ModelState.IsValid)
            {
                _context.IncomeTypeModel.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("IncomeType");
        }

    }
}