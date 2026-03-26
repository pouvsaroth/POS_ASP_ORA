using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly OracleDbHelper _db;

        public SupplierService(OracleDbHelper db)
        {
            _db = db;
        }

        public List<Supplier> GetSuppliers()
        {
            var list = new List<Supplier>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", "GET"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_SUPPLIERNAME", DBNull.Value),
                new OracleParameter("P_SEX", DBNull.Value),
                new OracleParameter("P_PHONE", DBNull.Value),
                new OracleParameter("P_EMAIL", DBNull.Value),
                new OracleParameter("P_ADDRESS", DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                { Direction = ParameterDirection.Output }
            };

            var dt = _db.ExecuteQuery("SP_SUPPLIER_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Supplier
                {
                    Id = Convert.ToInt32(row["ID"]),
                    SupplierName = row["SUPPLIERNAME"].ToString(),
                    Sex = row["SEX"].ToString(),
                    Phone = row["PHONE"].ToString(),
                    Email = row["EMAIL"].ToString(),
                    Address = row["ADDRESS"].ToString()
                });
            }

            return list;
        }

        public string InsertSupplier(Supplier model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_SUPPLIERNAME",model.SupplierName),
                    new OracleParameter("P_SEX",model.Sex),
                    new OracleParameter("P_PHONE",model.Phone),
                    new OracleParameter("P_EMAIL",model.Email),
                    new OracleParameter("P_ADDRESS",model.Address),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_SUPPLIER_CRUD", parameters);
                return "Supplier saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        public string UpdateSupplier(Supplier model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID",model.Id),
                    new OracleParameter("P_SUPPLIERNAME",model.SupplierName),
                    new OracleParameter("P_SEX",model.Sex),
                    new OracleParameter("P_PHONE",model.Phone),
                    new OracleParameter("P_EMAIL",model.Email),
                    new OracleParameter("P_ADDRESS",model.Address),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_SUPPLIER_CRUD", parameters);
                return "Supplier updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        public string DeleteSupplier(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID",id),
                    new OracleParameter("P_SUPPLIERNAME",DBNull.Value),
                    new OracleParameter("P_SEX",DBNull.Value),
                    new OracleParameter("P_PHONE",DBNull.Value),
                    new OracleParameter("P_EMAIL",DBNull.Value),
                    new OracleParameter("P_ADDRESS",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_SUPPLIER_CRUD", parameters);
                return "Supplier deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}