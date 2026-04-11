using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IPriceManagementService
    {
        List<PriceManagementModel> GetProductPrices();
        string InsertProductPrice(PriceManagementModel model);
        string UpdateProductPrice(PriceManagementModel model);
        string DeleteProductPrice(int id);
    }
}
