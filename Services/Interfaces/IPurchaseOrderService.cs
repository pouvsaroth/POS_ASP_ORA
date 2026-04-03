using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        List<Purchase> GetPurchases();
        string SavePurchase(Purchase model);
    }
}
