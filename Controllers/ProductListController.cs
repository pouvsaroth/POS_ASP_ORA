using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;


namespace POS_ASP_ORA.Controllers
{
    public class ProductListController:Controller
    {
        private readonly IConfiguration _config;

        private readonly IProductListService _service;
        private readonly IProductCategoryService _categoryService;
        private readonly ISupplierService _supplierService;

        public ProductListController(IProductListService service, IProductCategoryService categoryService, ISupplierService supplierService, IConfiguration config)
        {
            _service = service;
            _categoryService = categoryService;
            _supplierService= supplierService;
            _config = config;
        }

        // =========================
        // VIEW (LIST PAGE)
        // =========================
        public IActionResult ViewProductList()
        {
            var products = _service.GetProducts();
            return View("~/Views/Product/ProductList.cshtml", products);
        }

        // =========================
        // GET CATEGORY (FOR SELECT2)
        // =========================
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();

            // return only needed fields
            var result = categories.Select(c => new
            {
                id = c.Id,
                categoryName = c.CategoryName
            });

            return Json(result);
        }
        // GET CATEGORY (FOR SELECT2)
        // =========================
        [HttpGet]
        public IActionResult GetSupplier()
        {
            var suppliers = _supplierService.GetSuppliers();

            // return only needed fields
            var result = suppliers.Select(c => new
            {
                id = c.Id,
                supplierName = c.SupplierName
            });

            return Json(result);
        }

        // =========================
        // CREATE (AJAX)
        // =========================
        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (model == null)
                return Json("Invalid data");

            //if (model.ImageFile != null)
            //{
            //    // get path from appsettings
            //    var folderName = _config["FileUpload:ProductImagePath"];

            //    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            //    // create folder if not exists
            //    if (!Directory.Exists(uploadPath))
            //    {
            //        Directory.CreateDirectory(uploadPath);
            //    }

            //    // generate unique file name
            //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

            //    var filePath = Path.Combine(uploadPath, fileName);

            //    // save file
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.ImageFile.CopyToAsync(stream);
            //    }

            //    // save relative path to DB
            //    model.ImagePath = "/" + folderName + "/" + fileName;
            //}

            var result = _service.InsertProduct(model);
            return Json(result);
        }

        // =========================
        // UPDATE (AJAX)
        // =========================
        [HttpPost]
        public IActionResult Update(Product model)
        {
            if (model == null || model.Id == 0)
                return Json("Invalid data");

            var result = _service.UpdateProduct(model);
            return Json(result);
        }

        // =========================
        // DELETE SINGLE
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return Json("Invalid ID");

            var result = _service.DeleteProduct(id);
            return Json(result);
        }

        // =========================
        // DELETE MULTIPLE (AJAX)
        // =========================
        [HttpPost]
        public IActionResult DeleteMultiple(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return Json("No items selected");

            var result = _service.DeleteMultiple(ids);
            return Json(result);
        }
    }
}
