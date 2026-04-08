using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        List<PurchaseModel> GetPurchases();
        (int id, string billNo) InsertPurchase(PurchaseModel model);
        void InsertDetail(PurchaseDetailModel detail);
        void InsertPayment(int purchaseId, decimal paid);
        string DeletePurchase(int id);
    }
}
