using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IProductUnitService
    {
        List<ProductUnit> GetProductUnits();
        string InsertProductUnit(ProductUnit model);
        string UpdateProductUnit(ProductUnit model);
        string DeleteProductUnit(int id);
    }
}
