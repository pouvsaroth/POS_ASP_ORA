using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;
namespace POS_ASP_ORA.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly OracleDbHelper _db;

        public PurchaseOrderService(OracleDbHelper db)
        {
            _db = db;
        }

        // Get all purchases
        public List<Purchase> GetPurchases()
        {
            List<Purchase> list = new List<Purchase>();

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION", "GET"),
                    new OracleParameter("P_ID", DBNull.Value),
                    new OracleParameter("P_SUPPLIERID", DBNull.Value),
                    new OracleParameter("P_BILLNO", DBNull.Value),
                    new OracleParameter("P_PURCHASEDATE", DBNull.Value),
                    new OracleParameter("P_TOTAL", DBNull.Value),
                    new OracleParameter("P_DISCOUNT", DBNull.Value),
                    new OracleParameter("P_PAID", DBNull.Value),
                    new OracleParameter("P_PRODUCTID", DBNull.Value),
                    new OracleParameter("P_QTY", DBNull.Value),
                    new OracleParameter("P_COST", DBNull.Value),

                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

            DataTable dt = _db.ExecuteQuery("SP_PURCHASE_CRUD", parameters);

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

            return list;
        }

        // Save purchase (insert master, details, and payment)
        public string SavePurchase(Purchase model)
        {
            try
            {
                // Insert purchase (master)
                var parameters = new List<OracleParameter>
                    {
                        new OracleParameter("P_ACTION", "INSERT"),
                        new OracleParameter("P_SUPPLIERID", model.SupplierId),
                        new OracleParameter("P_BILLNO", model.BillNo),
                        new OracleParameter("P_PURCHASEDATE", model.PurchaseDate),
                        new OracleParameter("P_TOTAL", model.Items.Sum(x => x.Qty * x.Cost)),
                        new OracleParameter("P_DISCOUNT", model.Discount),
                        new OracleParameter("P_CURSOR", OracleDbType.RefCursor) { Direction = ParameterDirection.Output }
                    };

                DataTable dt = _db.ExecuteQuery("SP_PURCHASE_CRUD", parameters);
                int purchaseId = Convert.ToInt32(dt.Rows[0]["ID"]);

                // Insert purchase details
                foreach (var item in model.Items)
                {
                    var detailParams = new List<OracleParameter>
                    {
                        new OracleParameter("P_ACTION", "INSERT_DETAIL"),
                        new OracleParameter("P_ID", purchaseId),
                        new OracleParameter("P_PRODUCTID", item.ProductId),
                        new OracleParameter("P_QTY", item.Qty),
                        new OracleParameter("P_COST", item.Cost),
                        new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };

                    _db.ExecuteQuery("SP_PURCHASE_CRUD", detailParams);
                }

                // Insert payment
                if (model.Paid > 0)
                {
                    var payParams = new List<OracleParameter>
                    {
                        new OracleParameter("P_ACTION", "INSERT_PAYMENT"),
                        new OracleParameter("P_ID", purchaseId),
                        new OracleParameter("P_PAID", model.Paid),
                        new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };

                    _db.ExecuteQuery("SP_PURCHASE_CRUD", payParams);
                }

                return "Purchase saved successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Delete purchase
        public string DeletePurchase(int purchaseId)
        {
            try
            {
                var parameters = new List<OracleParameter>
                    {
                        new OracleParameter("P_ACTION", "DELETE"),
                        new OracleParameter("P_ID", purchaseId),
                        new OracleParameter("P_CURSOR", OracleDbType.RefCursor) { Direction = ParameterDirection.Output }
                    };

                _db.ExecuteNonQuery("SP_PURCHASE_CRUD", parameters);

                return "Purchase deleted successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
