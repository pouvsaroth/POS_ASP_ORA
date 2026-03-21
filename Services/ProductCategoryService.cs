using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using System.Data;

namespace POS_ASP_ORA.Services
{    
    public class ProductCategoryService
    {
        private readonly OracleDbHelper _db;

        public ProductCategoryService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET CATEGORY
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();

            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","GET"),
                    new OracleParameter("P_ID",DBNull.Value),
                    new OracleParameter("P_CATEGORYNAME",DBNull.Value),
                    new OracleParameter("P_STATUS",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                DataTable dt = _db.ExecuteQuery("SP_CATEGORY_CRUD", parameters);

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Category
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        CategoryName = row["CATEGORYNAME"].ToString(),
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

        // INSERT
        public string InsertCategory(Category model)
        {
            try
            {
                var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","INSERT"),
                new OracleParameter("P_ID",DBNull.Value),
                new OracleParameter("P_CATEGORYNAME",model.CategoryName),
                new OracleParameter("P_STATUS",model.Status),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                _db.ExecuteNonQuery("SP_CATEGORY_CRUD", parameters);

                return "Category saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        // UPDATE
        public string UpdateCategory(Category model)
        {
            try
            {
                var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","UPDATE"),
                new OracleParameter("P_ID",model.Id),
                new OracleParameter("P_CATEGORYNAME",model.CategoryName),
                new OracleParameter("P_STATUS",model.Status),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                _db.ExecuteNonQuery("SP_CATEGORY_CRUD", parameters);

                return "Category updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        // DELETE
        public string DeleteCategory(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","DELETE"),
                new OracleParameter("P_ID",id),
                new OracleParameter("P_CATEGORYNAME",DBNull.Value),
                new OracleParameter("P_STATUS",DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                _db.ExecuteNonQuery("SP_CATEGORY_CRUD", parameters);

                return "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}
