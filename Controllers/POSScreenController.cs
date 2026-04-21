using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;

namespace POS_ASP_ORA.Controllers
{
    public class POSScreenController:Controller
    {
        private readonly IProductCategoryService _categoryService;
        private readonly IProductListService _productListService;
        private readonly IConfiguration _config;
        private readonly IPOSScreenService _posScreenService;
        public POSScreenController(IProductCategoryService categoryService, IProductListService productListService, IConfiguration config, IPOSScreenService posScreenService)
        {
            _categoryService = categoryService;
            _productListService = productListService;
            _config = config;
            _posScreenService = posScreenService;
        }

        // LIST
        public IActionResult ViewPOSScreen()
        {
            var model = new POSScreenModel
            {
                Products = _productListService.GetProducts(),
                Categories = _categoryService.GetCategories(),
                ImageBasePath = _config["FileUpload:ProductImagePath"]
            };
            
            return View("~/Views/Sales/POSScreen.cshtml",model);
        }

        [HttpPost]
        public IActionResult SaveInvoice([FromBody] Sale model)
        {
            if (model == null || model.Details == null || model.Details.Count == 0)
                return Json("Invalid");

            var result = _posScreenService.SaveSales(model);

            return Json(result);
        }
    }
}
