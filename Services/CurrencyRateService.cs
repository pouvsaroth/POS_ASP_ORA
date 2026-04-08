using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly OracleDbHelper _db;

        public CurrencyRateService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET
        public List<CurrencyRateModel> GetRates()
        {
            List<CurrencyRateModel> list = new List<CurrencyRateModel>();

            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","GET"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_FROM_ID",DBNull.Value),
                    new OracleParameter("P_TO_ID",DBNull.Value),
                    new OracleParameter("P_RATE",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                DataTable dt = _db.ExecuteQuery("SP_CURRENCY_RATE_CRUD", parameters);

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CurrencyRateModel
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        FromCurrency = Convert.ToInt32(row["FROM_CURRENCY"]),
                        ToCurrency = Convert.ToInt32(row["TO_CURRENCY"]),
                        Rate = Convert.ToDecimal(row["RATE"]),
                        FromCode = row["FROM_CODE"].ToString(),
                        ToCode = row["TO_CODE"].ToString()
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
        public string InsertRate(CurrencyRateModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_FROM_ID",model.FromCurrency),
                    new OracleParameter("P_TO_ID",model.ToCurrency),
                    new OracleParameter("P_RATE",model.Rate),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_RATE_CRUD", parameters);

                return "Rate saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        // UPDATE
        public string UpdateRate(CurrencyRateModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_FROM_ID",model.FromCurrency),
                    new OracleParameter("P_TO_ID",model.ToCurrency),
                    new OracleParameter("P_RATE",model.Rate),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_RATE_CRUD", parameters);

                return "Rate updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        // DELETE
        public string DeleteRate(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_FROM_ID",DBNull.Value),
                    new OracleParameter("P_TO_ID",DBNull.Value),
                    new OracleParameter("P_RATE",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_CURRENCY_RATE_CRUD", parameters);

                return "Rate deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}