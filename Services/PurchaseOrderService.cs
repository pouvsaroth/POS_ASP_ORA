using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;
namespace POS_ASP_ORA.Services
{
    public class PurchaseOrderService:IPurchaseOrderService
    {
        private readonly OracleDbHelper _db;

        public PurchaseOrderService(OracleDbHelper db)
        {
            _db = db;
        }
        public List<Purchase> GetPurchases()
        {
            List<Purchase> list = new List<Purchase>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_GET_PURCHASES", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Purchase
                {
                    Id = Convert.ToInt32(row["ID"]),
                    BillNo = row["BILLNO"].ToString(),
                    SupplierName = row["SUPPLIERNAME"].ToString(),
                    PurchaseDate = Convert.ToDateTime(row["PURCHASEDATE"]),
                    Total = Convert.ToDecimal(row["TOTALAMOUNT"]),
                    Paid = row["PAID"] != DBNull.Value ? Convert.ToDecimal(row["PAID"]) : 0,
                    Status = Convert.ToInt32(row["STATUS"])
                });
            }

            return list; // NEVER return null
        }

        public string SavePurchase(Purchase model)
        {
            try
            {
                // 1. INSERT PURCHASE
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_SUPPLIERID", model.SupplierId),
                    new OracleParameter("P_BILLNO", model.BillNo),
                    new OracleParameter("P_PURCHASEDATE", model.PurchaseDate),
                    new OracleParameter("P_TOTAL", model.Items.Sum(x => x.Qty * x.Cost)),
                    new OracleParameter("P_DISCOUNT", model.Discount),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                DataTable dt = _db.ExecuteQuery("SP_PURCHASE_CRUD", parameters);

                int purchaseId = Convert.ToInt32(dt.Rows[0]["ID"]);

                // 2. INSERT DETAILS
                foreach (var item in model.Items)
                {
                    var detailParams = new List<OracleParameter>
                    {
                        new OracleParameter("P_PURCHASEID", purchaseId),
                        new OracleParameter("P_PRODUCTID", item.ProductId),
                        new OracleParameter("P_QTY", item.Qty),
                        new OracleParameter("P_COST", item.Cost)
                    };

                    _db.ExecuteNonQuery("SP_INSERT_PURCHASE_DETAIL", detailParams);
                }

                // 3. PAYMENT
                if (model.Paid > 0)
                {
                    var payParams = new List<OracleParameter>
                    {
                        new OracleParameter("P_PURCHASEID", purchaseId),
                        new OracleParameter("P_AMOUNT", model.Paid)
                    };

                    _db.ExecuteNonQuery("SP_INSERT_PURCHASE_PAYMENT", payParams);
                }

                return "Purchase saved successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
