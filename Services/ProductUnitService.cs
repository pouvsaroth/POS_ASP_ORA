using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class ProductUnitService : IProductUnitService
    {
        private readonly OracleDbHelper _db;

        public ProductUnitService(OracleDbHelper db)
        {
            _db = db;
        }

        public List<ProductUnit> GetProductUnits()
        {
            var list = new List<ProductUnit>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_UNIT_NAME", DBNull.Value),
                new OracleParameter("P_UNITTYPE_ID", DBNull.Value),
                new OracleParameter("P_QTY_PER_UNIT", DBNull.Value),
                new OracleParameter("P_REMARK", DBNull.Value),
                new OracleParameter("P_STATUS", DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                { Direction = ParameterDirection.Output }
            };

            var dt = _db.ExecuteQuery("SP_PRODUCTUNIT_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ProductUnit
                {
                    Id = Convert.ToInt32(row["ID"]),
                    UnitName = row["UNIT_NAME"].ToString(),
                    UnitTypeId = row["UNITTYPE_ID"] == DBNull.Value ? null : Convert.ToInt32(row["UNITTYPE_ID"]),
                    QtyPerUnit = row["QTY_PER_UNIT"] == DBNull.Value ? null : Convert.ToDecimal(row["QTY_PER_UNIT"]),
                    Remark = row["REMARK"].ToString(),
                    Status = row["STATUS"] == DBNull.Value ? null : Convert.ToInt32(row["STATUS"])
                });
            }

            return list;
        }

        public string InsertProductUnit(ProductUnit model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID", DBNull.Value),
                    new OracleParameter("P_UNIT_NAME", model.UnitName),
                    new OracleParameter("P_UNITTYPE_ID", model.UnitTypeId),
                    new OracleParameter("P_QTY_PER_UNIT", model.QtyPerUnit),
                    new OracleParameter("P_REMARK", model.Remark),
                    new OracleParameter("P_STATUS", model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTUNIT_CRUD", parameters);
                return "Product Unit saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        public string UpdateProductUnit(ProductUnit model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID", model.Id),
                    new OracleParameter("P_UNIT_NAME", model.UnitName),
                    new OracleParameter("P_UNITTYPE_ID", model.UnitTypeId),
                    new OracleParameter("P_QTY_PER_UNIT", model.QtyPerUnit),
                    new OracleParameter("P_REMARK", model.Remark),
                    new OracleParameter("P_STATUS", model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTUNIT_CRUD", parameters);
                return "Product Unit updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        public string DeleteProductUnit(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID", id),
                    new OracleParameter("P_UNIT_NAME", DBNull.Value),
                    new OracleParameter("P_UNITTYPE_ID", DBNull.Value),
                    new OracleParameter("P_QTY_PER_UNIT", DBNull.Value),
                    new OracleParameter("P_REMARK", DBNull.Value),
                    new OracleParameter("P_STATUS", DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_PRODUCTUNIT_CRUD", parameters);
                return "Product Unit deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}