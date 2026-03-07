using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Data;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Product()
        {
            var products = _context.ProductModel.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                _context.ProductModel.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("Product");
        }

        public IActionResult ProductCategory()
        {
            var list = _context.CategoryModel.ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.CategoryModel.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("Category");
        }
    }
}