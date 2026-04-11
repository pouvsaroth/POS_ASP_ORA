using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class PriceManagementService: IPriceManagementService
    {
        private readonly OracleDbHelper _db;

        public PriceManagementService(OracleDbHelper db)
        {
            _db = db;
        }

        public List<PriceManagementModel> GetProductPrices()
        {
            var list = new List<PriceManagementModel>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_PRODUCTID", DBNull.Value),
                new OracleParameter("P_SALEPRICE", DBNull.Value),
                new OracleParameter("P_CURRENCYID", DBNull.Value),
                new OracleParameter("P_CHANGEDBY", DBNull.Value),
                new OracleParameter("P_REMARK", DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                { Direction = ParameterDirection.Output }
            };

            var dt = _db.ExecuteQuery("SP_PRODUCTPRICE_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PriceManagementModel
                {
                    Id = Convert.ToInt32(row["ID"]),
                    ProductId = Convert.ToInt32(row["PRODUCTID"]),
                    ProductName = row["PRODUCTNAME"].ToString(),
                    SalePrice = Convert.ToDecimal(row["SALEPRICE"]),
                    CurrencyId = Convert.ToInt32(row["CURRENCYID"]),
                    CurrencyName = row["CURRENCYNAME"].ToString(),
                    ChangedDate = Convert.ToDateTime(row["CHANGEDDATE"]),
                    ChangedBy = row["CHANGEDBY"].ToString(),
                    Remark = row["REMARK"].ToString()
                });
            }

            return list;
        }

        public string InsertProductPrice(PriceManagementModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID", DBNull.Value),
                    new OracleParameter("P_PRODUCTID", model.ProductId),
                    new OracleParameter("P_SALEPRICE", model.SalePrice),
                    new OracleParameter("P_CURRENCYID", model.CurrencyId),
                    new OracleParameter("P_CHANGEDBY", model.ChangedBy),
                    new OracleParameter("P_REMARK", model.Remark),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTPRICE_CRUD", parameters);
                return "Product price saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        public string UpdateProductPrice(PriceManagementModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID", model.Id),
                    new OracleParameter("P_PRODUCTID", model.ProductId),
                    new OracleParameter("P_SALEPRICE", model.SalePrice),
                    new OracleParameter("P_CURRENCYID", model.CurrencyId),
                    new OracleParameter("P_CHANGEDBY", model.ChangedBy),
                    new OracleParameter("P_REMARK", model.Remark),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTPRICE_CRUD", parameters);
                return "Product price updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        public string DeleteProductPrice(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID", id),
                    new OracleParameter("P_PRODUCTID", DBNull.Value),
                    new OracleParameter("P_SALEPRICE", DBNull.Value),
                    new OracleParameter("P_CURRENCYID", DBNull.Value),
                    new OracleParameter("P_CHANGEDBY", DBNull.Value),
                    new OracleParameter("P_REMARK", DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTPRICE_CRUD", parameters);
                return "Product price deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}
