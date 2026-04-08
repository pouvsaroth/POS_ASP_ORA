using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly OracleDbHelper _db;

        public UnitTypeService(OracleDbHelper db)
        {
            _db = db;
        }

        public List<UnitType> GetUnitTypes()
        {
            List<UnitType> list = new();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET"),
                new OracleParameter("P_ID",DBNull.Value),
                new OracleParameter("P_NAME",DBNull.Value),
                new OracleParameter("P_STATUS",DBNull.Value),
                new OracleParameter("P_CURSOR",OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_UNITTYPE_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new UnitType
                {
                    Id = Convert.ToInt32(row["ID"]),
                    UnitTypeName = row["UNITTYPENAME"].ToString(),
                    Status = Convert.ToInt32(row["STATUS"])
                });
            }

            return list;
        }

        public string InsertUnitType(UnitType model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_NAME",model.UnitTypeName),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR",OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_UNITTYPE_CRUD", parameters);

                return "UnitType saved successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateUnitType(UnitType model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_NAME",model.UnitTypeName),
                    new OracleParameter("P_STATUS",model.Status),
                    new OracleParameter("P_CURSOR",OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_UNITTYPE_CRUD", parameters);

                return "UnitType updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteUnitType(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_NAME",DBNull.Value),
                    new OracleParameter("P_STATUS",DBNull.Value),
                    new OracleParameter("P_CURSOR",OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_UNITTYPE_CRUD", parameters);

                return "UnitType deleted successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}