using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly OracleDbHelper _db;

        public CompanyService(OracleDbHelper db)
        {
            _db = db;
        }

        public List<Company> GetCompanies()
        {
            var list = new List<Company>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", "GET"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_COMPANYNAME", DBNull.Value),
                new OracleParameter("P_LOCATION", DBNull.Value),
                new OracleParameter("P_PHONE", DBNull.Value),
                new OracleParameter("P_REMARK", DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                { Direction = ParameterDirection.Output }
            };

            var dt = _db.ExecuteQuery("SP_COMPANY_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Company
                {
                    Id = Convert.ToInt32(row["ID"]),
                    CompanyName = row["COMPANYNAME"].ToString(),
                    Location = row["LOCATION"].ToString(),
                    Phone = row["PHONE"].ToString(),
                    Remark = row["REMARK"].ToString()
                });
            }

            return list;
        }

        public string InsertCompany(Company model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_COMPANYNAME",model.CompanyName),
                    new OracleParameter("P_LOCATION",model.Location),
                    new OracleParameter("P_PHONE",model.Phone),
                    new OracleParameter("P_REMARK",model.Remark),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_COMPANY_CRUD", parameters);
                return "Company saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        public string UpdateCompany(Company model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_COMPANYNAME",model.CompanyName),
                    new OracleParameter("P_LOCATION",model.Location),
                    new OracleParameter("P_PHONE",model.Phone),
                    new OracleParameter("P_REMARK",model.Remark),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_COMPANY_CRUD", parameters);
                return "Company updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        public string DeleteCompany(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_COMPANYNAME",DBNull.Value),
                    new OracleParameter("P_LOCATION",DBNull.Value),
                    new OracleParameter("P_PHONE",DBNull.Value),
                    new OracleParameter("P_REMARK",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_COMPANY_CRUD", parameters);
                return "Company deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}
