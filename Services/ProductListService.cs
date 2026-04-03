using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class ProductListService: IProductListService
    {
        private readonly OracleDbHelper _db;

        public ProductListService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET
        public List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_PRODUCTCODE", DBNull.Value),
                new OracleParameter("P_BARCODE", DBNull.Value),
                new OracleParameter("P_PRODUCTNAME", DBNull.Value),
                new OracleParameter("P_PRODUCTNAMEKH", DBNull.Value),
                new OracleParameter("P_CATEGORYID", DBNull.Value),
                new OracleParameter("P_SUPPLIERID", DBNull.Value),
                new OracleParameter("P_QTYALERT", DBNull.Value),
                new OracleParameter("P_DESCRIPTION", DBNull.Value),
                new OracleParameter("P_IMAGENAME", DBNull.Value),
                new OracleParameter("P_STATUS", DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_PRODUCT_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Product
                {
                    Id = Convert.ToInt32(row["ID"]),
                    ProductCode = row["PRODUCTCODE"].ToString(),
                    Barcode = row["BARCODE"].ToString(),
                    ProductName = row["PRODUCTNAME"].ToString(),
                    ProductNameKh = row["PRODUCTNAMEKH"].ToString(),
                    CategoryId = Convert.ToInt32(row["CATEGORYID"]),
                    CategoryName = row["CATEGORYNAME"].ToString(),
                    SupplierId = Convert.ToInt32(row["SUPPLIERID"]),
                    SupplierName= row["SUPPLIERNAME"].ToString(),
                    QtyOnHand = row["QTYONHAND"] != DBNull.Value ? Convert.ToDecimal(row["QTYONHAND"]) : (decimal?)null,
                    QtyAlert = row["QTYALERT"] != DBNull.Value ? Convert.ToInt32(row["QTYALERT"]) : (int?)null,
                    ImageName = row["IMAGENAME"].ToString(),
                    Description = row["DESCRIPTION"] != DBNull.Value ? row["DESCRIPTION"].ToString() : null,
                    Status = Convert.ToInt32(row["STATUS"])
                });
            }

            return list;
        }

        // INSERT
        public string InsertProduct(Product model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","INSERT"),
                    new OracleParameter("P_ID", DBNull.Value),
                    new OracleParameter("P_PRODUCTCODE", model.ProductCode),
                    new OracleParameter("P_BARCODE", model.Barcode),
                    new OracleParameter("P_PRODUCTNAME", model.ProductName),
                    new OracleParameter("P_PRODUCTNAMEKH", model.ProductNameKh),
                    new OracleParameter("P_CATEGORYID", model.CategoryId),
                    new OracleParameter("P_SUPPLIERID", model.SupplierId),
                    new OracleParameter("P_QTYALERT", model.QtyAlert),
                    new OracleParameter("P_DESCRIPTION", model.Description),
                    new OracleParameter("P_IMAGENAME", model.ImageName),
                    new OracleParameter("P_STATUS", model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_PRODUCT_CRUD", parameters);
                return "Product saved successfully.";
            }
            catch (Exception ex)
            {
                return "Insert failed: " + ex.Message;
            }
        }

        // UPDATE
        public string UpdateProduct(Product model)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","UPDATE"),
                    new OracleParameter("P_ID", model.Id),
                    new OracleParameter("P_PRODUCTCODE", model.ProductCode),
                    new OracleParameter("P_BARCODE", model.Barcode),
                    new OracleParameter("P_PRODUCTNAME", model.ProductName),
                    new OracleParameter("P_PRODUCTNAMEKH", model.ProductNameKh),
                    new OracleParameter("P_CATEGORYID", model.CategoryId),
                    new OracleParameter("P_SUPPLIERID", model.SupplierId),
                    new OracleParameter("P_QTYALERT", model.QtyAlert),
                    new OracleParameter("P_DESCRIPTION", model.Description),
                    new OracleParameter("P_IMAGENAME", model.ImageName),
                    new OracleParameter("P_STATUS", model.Status),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_PRODUCT_CRUD", parameters);
                return "Product updated successfully.";
            }
            catch (Exception ex)
            {
                return "Update failed: " + ex.Message;
            }
        }

        // DELETE
        public string DeleteProduct(int id)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE"),
                    new OracleParameter("P_ID", id),
                    new OracleParameter("P_PRODUCTCODE", DBNull.Value),
                    new OracleParameter("P_BARCODE", DBNull.Value),
                    new OracleParameter("P_PRODUCTNAME", DBNull.Value),
                    new OracleParameter("P_PRODUCTNAMEKH", DBNull.Value),
                    new OracleParameter("P_CATEGORYID", DBNull.Value),
                    new OracleParameter("P_SUPPLIERID", DBNull.Value),
                    new OracleParameter("P_QTYALERT", DBNull.Value),
                    new OracleParameter("P_DESCRIPTION", DBNull.Value),
                    new OracleParameter("P_IMAGENAME", DBNull.Value),
                    new OracleParameter("P_STATUS", DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_PRODUCT_CRUD", parameters);
                return "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }

        // DELETE MULTIPLE
        public string DeleteMultiple(List<int> ids)
        {
            try
            {
                string idStr = string.Join(",", ids);

                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE_MULTIPLE"),
                    new OracleParameter("P_ID", idStr),
                    new OracleParameter("P_PRODUCTCODE", DBNull.Value),
                    new OracleParameter("P_BARCODE", DBNull.Value),
                    new OracleParameter("P_PRODUCTNAME", DBNull.Value),
                    new OracleParameter("P_PRODUCTNAMEKH", DBNull.Value),
                    new OracleParameter("P_CATEGORYID", DBNull.Value),
                    new OracleParameter("P_SUPPLIERID", DBNull.Value),
                    new OracleParameter("P_QTYALERT", DBNull.Value),
                    new OracleParameter("P_DESCRIPTION", DBNull.Value),
                    new OracleParameter("P_IMAGENAME", DBNull.Value),
                    new OracleParameter("P_STATUS", DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_PRODUCT_CRUD", parameters);
                return "true";
            }
            catch (Exception ex)
            {
                return "Delete failed: " + ex.Message;
            }
        }
    }
}
