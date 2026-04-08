using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _config;

        private readonly IProductListService _service;
        private readonly IProductCategoryService _categoryService;
        private readonly ISupplierService _supplierService;

        public BaseController(IProductListService service, IProductCategoryService categoryService, ISupplierService supplierService, IConfiguration config)
        {
            _service = service;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _config = config;
        }

        public IActionResult GetProductlistDropDown()
        {
            var products = _service.GetProducts();
            var result = products.Select(c => new
            {
                id = c.Id,
                name = c.ProductName
            });

            return Json(result);
        }
        public IActionResult GetSupplierDropDown()
        {
            var products = _supplierService.GetSuppliers();
            var result = products.Select(c => new
            {
                id = c.Id,
                name = c.SupplierName
            });

            return Json(result);
        }
        public IActionResult GetProductUnitDropDown()
        {
            var products = _supplierService.GetSuppliers();
            var result = products.Select(c => new
            {
                id = c.Id,
                name = c.SupplierName
            });

            return Json(result);
        }
        public IActionResult GetUnitDropDown()
        {
            var products = _supplierService.GetSuppliers(); 
            var result = products.Select(c => new
            {
                id = c.Id,
                name = c.SupplierName
            });

            return Json(result);
        }
    }
}
