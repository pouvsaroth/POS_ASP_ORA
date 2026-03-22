using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class MenuService
    {
        private readonly OracleDbHelper _db;

        public MenuService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET
        public List<MenuModel> GetMenus()
        {
            var list = new List<MenuModel>();

            var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","GET"),
            new OracleParameter("P_ID",DBNull.Value),
            new OracleParameter("P_MENUNAME",DBNull.Value),
            new OracleParameter("P_CONTROLLERNAME",DBNull.Value),
            new OracleParameter("P_ACTIONNAME",DBNull.Value),
            new OracleParameter("P_ICON",DBNull.Value),
            new OracleParameter("P_PARENTID",DBNull.Value),
            new OracleParameter("P_DISPLAYORDER",DBNull.Value),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            }
        };

            DataTable dt = _db.ExecuteQuery("SP_MENU_CRUD", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MenuModel
                {
                    Id = Convert.ToInt32(row["ID"]),
                    MenuName = row["MENUNAME"].ToString(),
                    ControllerName = row["CONTROLLERNAME"]?.ToString(),
                    ActionName = row["ACTIONNAME"]?.ToString(),
                    Icon = row["ICON"]?.ToString(),
                    ParentId = row["PARENTID"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["PARENTID"]),
                    DisplayOrder = Convert.ToInt32(row["DISPLAYORDER"])
                });
            }

            return list;
        }

        // INSERT
        public string Insert(MenuModel model)
        {
            try
            {
                var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","INSERT"),
                new OracleParameter("P_ID",DBNull.Value),
                new OracleParameter("P_MENUNAME",model.MenuName),
                new OracleParameter("P_CONTROLLERNAME",model.ControllerName),
                new OracleParameter("P_ACTIONNAME",model.ActionName),
                new OracleParameter("P_ICON",model.Icon),
                new OracleParameter("P_PARENTID",(object?)model.ParentId ?? DBNull.Value),
                new OracleParameter("P_DISPLAYORDER",model.DisplayOrder),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

                _db.ExecuteNonQuery("SP_MENU_CRUD", parameters);
                return "Menu created successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // UPDATE
        public string Update(MenuModel model)
        {
            var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","UPDATE"),
            new OracleParameter("P_ID",model.Id),
            new OracleParameter("P_MENUNAME",model.MenuName),
            new OracleParameter("P_CONTROLLERNAME",model.ControllerName),
            new OracleParameter("P_ACTIONNAME",model.ActionName),
            new OracleParameter("P_ICON",model.Icon),
            new OracleParameter("P_PARENTID",(object?)model.ParentId ?? DBNull.Value),
            new OracleParameter("P_DISPLAYORDER",model.DisplayOrder),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            }
        };

            _db.ExecuteNonQuery("SP_MENU_CRUD", parameters);
            return "Menu updated successfully.";
        }

        // DELETE
        public string Delete(int id)
        {
            var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","DELETE"),
            new OracleParameter("P_ID",id),
            new OracleParameter("P_MENUNAME",DBNull.Value),
            new OracleParameter("P_CONTROLLERNAME",DBNull.Value),
            new OracleParameter("P_ACTIONNAME",DBNull.Value),
            new OracleParameter("P_ICON",DBNull.Value),
            new OracleParameter("P_PARENTID",DBNull.Value),
            new OracleParameter("P_DISPLAYORDER",DBNull.Value),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            }
        };

            _db.ExecuteNonQuery("SP_MENU_CRUD", parameters);
            return "Deleted successfully";
        }
    }
}
