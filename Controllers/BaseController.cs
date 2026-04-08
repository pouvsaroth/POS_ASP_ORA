using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _config;

        private readonly IProductListService _service;
        private readonly IProductCategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IProductUnitService _productUnitService;
        private readonly IUnitTypeService _unittypeService;
        private readonly ICurrencyService _currencyService;

        public BaseController(IProductListService service, IProductCategoryService categoryService, ISupplierService supplierService, IConfiguration config,IProductUnitService productUnitService, IUnitTypeService unitTypeService, ICurrencyService currencyService)
        {
            _service = service;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _config = config;
            _productUnitService = productUnitService;
            _unittypeService = unitTypeService;
            _currencyService = currencyService;
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
            var productUnit = _productUnitService.GetProductUnits();
            var result = productUnit.Select(c => new
            {
                id = c.Id,
                name = c.UnitName
            });

            return Json(result);
        }
        public IActionResult GetUnitDropDown()
        {
            var unit = _unittypeService.GetUnitTypes();
            var result = unit.Select(c => new
            {
                id = c.Id,
                name = c.UnitTypeName
            });

            return Json(result);
        }
        public IActionResult GetCurrencyDropDown()
        {
            var currency = _currencyService.GetCurrencies();
            var result = currency.Select(c => new
            {
                id = c.Id,
                name = c.Name
            });

            return Json(result);
        }
       
    }
}
