using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class POSScreenService: IPOSScreenService
    {
        private readonly OracleDbHelper _db;

        public POSScreenService(OracleDbHelper db)
        {
            _db = db;
        }

        public object SaveSales(Sale model)
        {
            int saleId = 0;
            string invoiceNo = GenerateInvoiceNo();
            try
            {
                _db.BeginTransaction();

                // =========================
                // 1. INSERT SALE
                // =========================
                var saleParams = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION", OracleDbType.Varchar2) { Value = "INSERT_SALE" },
            new OracleParameter("P_ID", OracleDbType.Int32) { Value = DBNull.Value },
            new OracleParameter("P_INVOICENO", OracleDbType.Varchar2) { Value = invoiceNo },
            new OracleParameter("P_CUSTOMERID", OracleDbType.Int32) { Value = model.CustomerId },
            new OracleParameter("P_TOTALAMOUNT", OracleDbType.Decimal) { Value = model.TotalAmount },
            new OracleParameter("P_DISCOUNT", OracleDbType.Decimal) { Value = model.Discount },
            new OracleParameter("P_STATUS", OracleDbType.Int32) { Value = model.Status },

            new OracleParameter("P_PRODUCTID", OracleDbType.Int32) { Value = DBNull.Value },
            new OracleParameter("P_QTY", OracleDbType.Decimal) { Value = DBNull.Value },
            new OracleParameter("P_COST", OracleDbType.Decimal) { Value = DBNull.Value },
            new OracleParameter("P_PRICE", OracleDbType.Decimal) { Value = DBNull.Value },
            new OracleParameter("P_SUBDISCOUNT", OracleDbType.Decimal) { Value = DBNull.Value },

            new OracleParameter("P_PAYMENTMETHOD", OracleDbType.Int32) { Value = DBNull.Value },
            new OracleParameter("P_PAYAMOUNT", OracleDbType.Decimal) { Value = DBNull.Value },

            new OracleParameter("P_SALEID", OracleDbType.Int32)
            {
                Value = saleId,
                Direction = ParameterDirection.InputOutput
            },

            new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            }
        };

                _db.ExecuteNonQuery("SP_SALE_CRUD", saleParams);

                saleId = Convert.ToInt32(
                    saleParams.First(x => x.ParameterName == "P_SALEID").Value.ToString()
                );

                // =========================
                // 2. INSERT DETAILS
                // =========================
                foreach (var item in model.Details)
                {
                    var detailParams = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", OracleDbType.Varchar2) { Value = "INSERT_DETAIL" },
                new OracleParameter("P_ID", OracleDbType.Int32) { Value = DBNull.Value },

                new OracleParameter("P_INVOICENO", OracleDbType.Varchar2) { Value = DBNull.Value },
                new OracleParameter("P_CUSTOMERID", OracleDbType.Int32) { Value = DBNull.Value },
                new OracleParameter("P_TOTALAMOUNT", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_DISCOUNT", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_STATUS", OracleDbType.Int32) { Value = DBNull.Value },

                new OracleParameter("P_PRODUCTID", OracleDbType.Int32) { Value = item.ProductId },
                new OracleParameter("P_QTY", OracleDbType.Decimal) { Value = item.Qty },
                new OracleParameter("P_COST", OracleDbType.Decimal) { Value = item.Cost },
                new OracleParameter("P_PRICE", OracleDbType.Decimal) { Value = item.Price },
                new OracleParameter("P_SUBDISCOUNT", OracleDbType.Decimal) { Value = item.SubDiscount },

                new OracleParameter("P_PAYMENTMETHOD", OracleDbType.Int32) { Value = DBNull.Value },
                new OracleParameter("P_PAYAMOUNT", OracleDbType.Decimal) { Value = DBNull.Value },

                new OracleParameter("P_SALEID", OracleDbType.Int32)
                {
                    Value = saleId,
                    Direction = ParameterDirection.InputOutput
                },

                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                    _db.ExecuteNonQuery("SP_SALE_CRUD", detailParams);
                }

                // =========================
                // 3. INSERT PAYMENT
                // =========================
                if (model.Payment != null)
                {
                    var payParams = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", OracleDbType.Varchar2) { Value = "INSERT_PAYMENT" },
                new OracleParameter("P_ID", OracleDbType.Int32) { Value = DBNull.Value },

                new OracleParameter("P_INVOICENO", OracleDbType.Varchar2) { Value = DBNull.Value },
                new OracleParameter("P_CUSTOMERID", OracleDbType.Int32) { Value = DBNull.Value },
                new OracleParameter("P_TOTALAMOUNT", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_DISCOUNT", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_STATUS", OracleDbType.Int32) { Value = DBNull.Value },

                new OracleParameter("P_PRODUCTID", OracleDbType.Int32) { Value = DBNull.Value },
                new OracleParameter("P_QTY", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_COST", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_PRICE", OracleDbType.Decimal) { Value = DBNull.Value },
                new OracleParameter("P_SUBDISCOUNT", OracleDbType.Decimal) { Value = DBNull.Value },

                new OracleParameter("P_PAYMENTMETHOD", OracleDbType.Int32) { Value = model.Payment.PaymentMethod },
                new OracleParameter("P_PAYAMOUNT", OracleDbType.Decimal) { Value = model.Payment.PayAmount },

                new OracleParameter("P_SALEID", OracleDbType.Int32)
                {
                    Value = saleId,
                    Direction = ParameterDirection.InputOutput
                },

                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                    _db.ExecuteNonQuery("SP_SALE_CRUD", payParams);
                }

                _db.Commit();

                return new
                {
                    Status = "SUCCESS",
                    InvoiceNo = invoiceNo,
                    SaleId = saleId
                };
            }
            catch (Exception ex)
            {
                _db.Rollback();
                return "ERROR: " + ex.Message;
            }
        }

        // =========================
        // 🔥 GENERATE INVOICE NO
        // =========================
        private string GenerateInvoiceNo()
        {
            var now = DateTime.Now;
            return "INV"
                + now.Year
                + now.Month.ToString("D2")
                + now.Day.ToString("D2")
                + now.Hour.ToString("D2")
                + now.Minute.ToString("D2")
                + now.Second.ToString("D2");
        }
    }
}
