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

        // =========================
        // GET PURCHASE
        // =========================
        public List<PurchaseModel> GetPurchases()
        {
            List<PurchaseModel> list = new List<PurchaseModel>();

            try
            {
                var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","GET"),
            new OracleParameter("P_ID",DBNull.Value),
            new OracleParameter("P_IDLIST", DBNull.Value ),
            new OracleParameter("P_SUPPLIERID",DBNull.Value),
            new OracleParameter("P_PURCHASEDATE",DBNull.Value),
            new OracleParameter("P_TOTAL",DBNull.Value),

            new OracleParameter("P_PRODUCTID",DBNull.Value),
            new OracleParameter("P_QTY",DBNull.Value),
            new OracleParameter("P_COST",DBNull.Value),
            new OracleParameter("P_PRODUCTUNITID",DBNull.Value),
            new OracleParameter("P_UNITID",DBNull.Value),
            new OracleParameter("P_VAT",DBNull.Value),
            new OracleParameter("P_CURRENCYID",DBNull.Value),
            new OracleParameter("P_DISCOUNT",DBNull.Value),

            new OracleParameter("P_PAID",DBNull.Value),

            new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            }
        };

                DataTable dt = _db.ExecuteQuery("SP_PURCHASE_CRUD", parameters);

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PurchaseModel
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        BillNo = row["BILLNO"].ToString(),
                        PurchaseDate = Convert.ToDateTime(row["PURCHASEDATE"]),
                        SupplierName = row["SUPPLIERNAME"].ToString(),
                        TotalAmount = Convert.ToDecimal(row["TOTALAMOUNT"]),
                        Paid = Convert.ToDecimal(row["PAID"]),
                        Status = Convert.ToInt32(row["STATUS"])
                    });
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        // =========================
        // INSERT MASTER
        // =========================
        public (int id, string billNo) InsertPurchase(PurchaseModel model)
        {
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","INSERT"),
                new OracleParameter("P_ID",DBNull.Value),
                new OracleParameter("P_IDLIST", DBNull.Value ),
                new OracleParameter("P_SUPPLIERID",model.SupplierId),
                new OracleParameter("P_PURCHASEDATE",model.PurchaseDate),
                new OracleParameter("P_TOTAL",model.TotalAmount),

                new OracleParameter("P_PRODUCTID",DBNull.Value),
                new OracleParameter("P_QTY",DBNull.Value),
                new OracleParameter("P_COST",DBNull.Value),
                new OracleParameter("P_PRODUCTUNITID",DBNull.Value),
                new OracleParameter("P_UNITID",DBNull.Value),
                new OracleParameter("P_VAT",DBNull.Value),
                new OracleParameter("P_CURRENCYID",DBNull.Value),
                new OracleParameter("P_DISCOUNT",DBNull.Value),

                new OracleParameter("P_PAID",DBNull.Value),

                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_PURCHASE_CRUD", parameters);

            int id = 0;
            string billNo = "";

            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["ID"]);
                billNo = dt.Rows[0]["BILLNO"].ToString();
            }

            return (id, billNo);
        }

        // =========================
        // INSERT DETAIL
        // =========================
        public void InsertDetail(PurchaseDetailModel d)
        {
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","INSERT_DETAIL"),
                new OracleParameter("P_ID",d.PurchaseId),
                new OracleParameter("P_IDLIST", DBNull.Value ),
                new OracleParameter("P_SUPPLIERID",DBNull.Value),
                new OracleParameter("P_PURCHASEDATE",DBNull.Value),
                new OracleParameter("P_TOTAL",DBNull.Value),

                new OracleParameter("P_PRODUCTID",d.ProductId),
                new OracleParameter("P_QTY",d.Qty),
                new OracleParameter("P_COST",d.Cost),
                new OracleParameter("P_PRODUCTUNITID",d.ProductUnitId),
                new OracleParameter("P_UNITID",d.UnitId),
                new OracleParameter("P_VAT",d.Vat),
                new OracleParameter("P_CURRENCYID",d.CurrencyId),
                new OracleParameter("P_DISCOUNT",d.Discount),

                new OracleParameter("P_PAID",DBNull.Value),

                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _db.ExecuteNonQuery("SP_PURCHASE_CRUD", parameters);
        }
       
        // =========================
        // INSERT PAYMENT
        // =========================
        public void InsertPayment(int purchaseId, decimal paid)
        {
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","INSERT_PAYMENT"),
                new OracleParameter("P_ID",purchaseId),
                new OracleParameter("P_IDLIST", DBNull.Value ),
                new OracleParameter("P_SUPPLIERID",DBNull.Value),
                new OracleParameter("P_PURCHASEDATE",DBNull.Value),
                new OracleParameter("P_TOTAL",DBNull.Value),

                new OracleParameter("P_PRODUCTID",DBNull.Value),
                new OracleParameter("P_QTY",DBNull.Value),
                new OracleParameter("P_COST",DBNull.Value),
                new OracleParameter("P_PRODUCTUNITID",DBNull.Value),
                new OracleParameter("P_UNITID",DBNull.Value),
                new OracleParameter("P_VAT",DBNull.Value),
                new OracleParameter("P_CURRENCYID",DBNull.Value),
                new OracleParameter("P_DISCOUNT",DBNull.Value),

                new OracleParameter("P_PAID",paid),

                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _db.ExecuteNonQuery("SP_PURCHASE_CRUD", parameters);
        }

        // =========================
        // DELETE
        // =========================
        public string DeleteMultiple(List<int> ids)
        {
            try
            {
                string idStr = string.Join(",", ids);

                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE_MULTIPLE"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_IDLIST", idStr ),
                    new OracleParameter("P_SUPPLIERID",DBNull.Value),
                    new OracleParameter("P_PURCHASEDATE",DBNull.Value),
                    new OracleParameter("P_TOTAL",DBNull.Value),

                    new OracleParameter("P_PRODUCTID",DBNull.Value),
                    new OracleParameter("P_QTY",DBNull.Value),
                    new OracleParameter("P_COST",DBNull.Value),
                    new OracleParameter("P_PRODUCTUNITID",DBNull.Value),
                    new OracleParameter("P_UNITID",DBNull.Value),
                    new OracleParameter("P_VAT",DBNull.Value),
                    new OracleParameter("P_CURRENCYID",DBNull.Value),
                    new OracleParameter("P_DISCOUNT",DBNull.Value),

                    new OracleParameter("P_PAID",DBNull.Value),

                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_PURCHASE_CRUD", parameters);

                return "Deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}