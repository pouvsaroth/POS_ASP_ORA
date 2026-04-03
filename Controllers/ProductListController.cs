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
        public async Task<IActionResult> Create([FromForm] Product model)
        {
            if (model == null)
                return Json("Invalid data");

            if (model.ImageFile != null)
            {
                // get path from appsettings
                var folderName = _config["FileUpload:ProductImagePath"];

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

                // create folder if not exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // generate unique file name
                model.ImageName = model.ProductCode + Path.GetExtension(model.ImageFile.FileName);

                var filePath = Path.Combine(uploadPath, model.ImageName);

                // save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
               
            }

            var result = _service.InsertProduct(model);
            return Json(result);
        }

        // =========================
        // UPDATE (AJAX)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Product model)
        {
            if (model == null || model.Id == 0)
                return Json("Invalid data");

            var folderName = _config["FileUpload:ProductImagePath"];
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // ✅ CASE 1: User uploads new image
            if (model.ImageFile != null)
            {
                // 🔥 Delete old image
                if (!string.IsNullOrEmpty(model.OldImageName))
                {
                    var oldFilePath = Path.Combine(uploadPath, model.OldImageName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // 🔥 Save new image
                var fileName = model.ProductCode + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                model.ImageName = fileName;
            }
            else
            {
                // ✅ CASE 2: No new image → keep old
                model.ImageName = model.OldImageName;
            }

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
        public IActionResult DeleteMultiple(List<int> ids, List<string> ImageNames)
        {
            

            if (ids == null || ids.Count == 0)
                return Json("No items selected");

            var result = _service.DeleteMultiple(ids);
            if (result == "true") {
                var folderName = _config["FileUpload:ProductImagePath"];
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                foreach (var item in ImageNames)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var filePath = Path.Combine(uploadPath, item);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
            }
            
            return Json(result);
        }
    }
}
