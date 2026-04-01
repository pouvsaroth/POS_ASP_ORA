using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IProductListService
    {

        // GET
        List<Product> GetProducts();

        // INSERT
        string InsertProduct(Product model);

        // UPDATE
        string UpdateProduct(Product model);

        // DELETE SINGLE
        string DeleteProduct(int id);

        // DELETE MULTIPLE
        string DeleteMultiple(List<int> ids);
    }
}
