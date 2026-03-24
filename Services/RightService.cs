using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class RightService : IRightService
    {
        private readonly OracleDbHelper _db;

        public RightService(OracleDbHelper db)
        {
            _db = db;
        }

        // GET GROUPS
        public List<RightModel> GetGroups()
        {
            List<RightModel> list = new List<RightModel>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET_GROUP"),
                new OracleParameter("P_GROUPID",DBNull.Value),
                new OracleParameter("P_MENU_ID",DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_RIGHT", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new RightModel
                {
                    GroupId = Convert.ToInt32(row["ID"]),
                    GroupName = row["GROUPNAME"].ToString()
                });
            }

            return list;
        }

        // GET MENUS BY GROUP
        public List<RightModel> GetMenusByGroup(int groupId)
        {
            List<RightModel> list = new List<RightModel>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION","GET_MENU"),
                new OracleParameter("P_GROUPID",groupId),
                new OracleParameter("P_MENU_ID",DBNull.Value),
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_RIGHT", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new RightModel
                {
                    MenuId = Convert.ToInt32(row["ID"]),
                    MenuName = row["MENUNAME"].ToString(),
                    ParentId = row["PARENTID"] == DBNull.Value ? null : Convert.ToInt32(row["PARENTID"]),
                    IsSelected = Convert.ToInt32(row["ISSELECTED"])
                });
            }

            return list;
        }

        // SAVE
        public string SaveGroupMenu(int groupId, List<int> menuIds)
        {
            try
            {
                // DELETE OLD
                var deleteParam = new List<OracleParameter>
                {
                    new OracleParameter("P_ACTION","DELETE_GROUP_MENU"),
                    new OracleParameter("P_GROUPID",groupId),
                    new OracleParameter("P_MENU_ID",DBNull.Value),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                _db.ExecuteNonQuery("SP_RIGHT", deleteParam);

                // INSERT NEW
                foreach (var menuId in menuIds)
                {
                    var insertParam = new List<OracleParameter>
                    {
                        new OracleParameter("P_ACTION","INSERT_GROUP_MENU"),
                        new OracleParameter("P_GROUPID",groupId),
                        new OracleParameter("P_MENU_ID",menuId),
                        new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };

                    _db.ExecuteNonQuery("SP_RIGHT", insertParam);
                }

                return "Saved successfully.";
            }
            catch (Exception ex)
            {
                return "Save failed: " + ex.Message;
            }
        }
    }
}