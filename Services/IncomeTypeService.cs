using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly OracleDbHelper _db;

        public IncomeTypeService(OracleDbHelper db)
        {
            _db = db;
        }

        //GET
        public List<IncomeType> GetIncomeTypes()
        {
            List<IncomeType> list = new List<IncomeType>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET"),
                new OracleParameter("P_ID",DBNull.Value),
                new OracleParameter("P_TYPENAME",DBNull.Value),
                new OracleParameter("P_STATUS",DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_INCOMETYPE_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new IncomeType
                {
                    Id = Convert.ToInt32(row["ID"]),
                    TypeName = row["TYPENAME"].ToString(),
                    Status = Convert.ToInt32(row["STATUS"])
                });
            }

            return list;
        }

        //INSERT
        public string InsertIncomeType(IncomeType model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_TYPENAME",model.TypeName),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_INCOMETYPE_CRUD", parameters);

                return "Income Type saved successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //UPDATE
        public string UpdateIncomeType(IncomeType model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_TYPENAME",model.TypeName),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_INCOMETYPE_CRUD", parameters);

                return "Income Type updated successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //DELETE
        public string DeleteIncomeType(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_TYPENAME",DBNull.Value),
                    new OracleParameter("P_STATUS",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_INCOMETYPE_CRUD", parameters);

                return "IncomeType deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}