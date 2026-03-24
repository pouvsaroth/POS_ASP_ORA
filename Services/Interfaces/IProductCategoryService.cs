using POS_ASP_ORA.Models;
using System.Collections.Generic;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IProductCategoryService
    {
        // GET
        List<Category> GetCategories();

        // INSERT
        string InsertCategory(Category model);

        // UPDATE
        string UpdateCategory(Category model);

        // DELETE
        string DeleteCategory(int id);
    }
}