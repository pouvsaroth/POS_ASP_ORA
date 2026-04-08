using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly OracleDbHelper _db;

        public CurrencyService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET
        public List<CurrencyModel> GetCurrencies()
        {
            List<CurrencyModel> list = new List<CurrencyModel>();

            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","GET"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_CODE",DBNull.Value),
                    new OracleParameter("P_NAME",DBNull.Value),
                    new OracleParameter("P_SYMBOL",DBNull.Value),
                    new OracleParameter("P_DECIMAL",DBNull.Value),
                    new OracleParameter("P_IS_BASE",DBNull.Value),
                    new OracleParameter("P_STATUS",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                DataTable dt = _db.ExecuteQuery("SP_CURRENCY_CRUD", parameters);

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CurrencyModel
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Code = row["CODE"].ToString(),
                        Name = row["NAME"].ToString(),
                        Symbol = row["SYMBOL"].ToString(),
                        DecimalPlaces = Convert.ToInt32(row["DECIMAL_PLACES"]),
                        IsBase = Convert.ToInt32(row["IS_BASE"]),
                        Status = Convert.ToInt32(row["IS_ACTIVE"])
                    });
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        // INSERT
        public string InsertCurrency(CurrencyModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_CODE",model.Code),
                    new OracleParameter("P_NAME",model.Name),
                    new OracleParameter("P_SYMBOL",model.Symbol),
                    new OracleParameter("P_DECIMAL",model.DecimalPlaces),
                    new OracleParameter("P_IS_BASE",model.IsBase),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_CRUD", parameters);

                return "Currency saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        // UPDATE
        public string UpdateCurrency(CurrencyModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_CODE",model.Code),
                    new OracleParameter("P_NAME",model.Name),
                    new OracleParameter("P_SYMBOL",model.Symbol),
                    new OracleParameter("P_DECIMAL",model.DecimalPlaces),
                    new OracleParameter("P_IS_BASE",model.IsBase),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_CRUD", parameters);

                return "Currency updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        // DELETE
        public string DeleteCurrency(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_CODE",DBNull.Value),
                    new OracleParameter("P_NAME",DBNull.Value),
                    new OracleParameter("P_SYMBOL",DBNull.Value),
                    new OracleParameter("P_DECIMAL",DBNull.Value),
                    new OracleParameter("P_IS_BASE",DBNull.Value),
                    new OracleParameter("P_STATUS",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_CRUD", parameters);

                return "Currency deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}