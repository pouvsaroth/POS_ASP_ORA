using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using System.Collections.Generic;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class AuthService
    {
        private readonly OracleDbHelper _db;

        public AuthService(OracleDbHelper db)
        {
            _db = db;
        }

        public (string Result, string UserId, int IsActive, string Email) Login(string username, string password)
        {
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", "SIGNIN"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_USERNAME", username),
                new OracleParameter("P_PASSWORD", password),
                new OracleParameter("P_EMAIL", DBNull.Value),
                new OracleParameter("P_IS_ACTIVE", DBNull.Value),
                new OracleParameter("P_CREATED_AT", DBNull.Value),
                new OracleParameter("P_UPDATED_AT", DBNull.Value),

                new OracleParameter("P_RESULT", OracleDbType.Varchar2, 200)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_USER_ID", OracleDbType.Varchar2, 50)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_IS_ACTIVE_OUT", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_EMAIL_OUT", OracleDbType.Varchar2, 200)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _db.ExecuteNonQuery("SP_USER_LOGIN_CRUD", parameters);

            return (
                parameters[8].Value.ToString(),
                parameters[9].Value?.ToString(),
                GeneralHelper.GetOracleInt(parameters[10].Value),
                parameters[11].Value?.ToString()
            );
        }

        public string RegisterUser(Users user)
        {
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_ACTION", "SIGNUP"),
                new OracleParameter("P_ID", DBNull.Value),
                new OracleParameter("P_USERNAME", user.Username),
                new OracleParameter("P_PASSWORD", user.Password),
                new OracleParameter("P_EMAIL", user.Email),
                new OracleParameter("P_IS_ACTIVE", user.IsActive ? 1 : 0),
                new OracleParameter("P_CREATED_AT", user.CreatedAt),
                new OracleParameter("P_UPDATED_AT", DBNull.Value),

                new OracleParameter("P_RESULT", OracleDbType.Varchar2, 200)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_USER_ID", OracleDbType.Varchar2, 50)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_IS_ACTIVE_OUT", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("P_EMAIL_OUT", OracleDbType.Varchar2, 200)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _db.ExecuteNonQuery("SP_USER_LOGIN_CRUD", parameters);

            return parameters[8].Value.ToString(); // P_RESULT
        }

        public List<MenuModel> GetUserMenu(string userId)
        {
            var list = new List<MenuModel>();

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("P_USERID", OracleDbType.Raw)
                {
                    Value = GeneralHelper.StringToByteArray(userId) // 🔥 convert to RAW
                },
                new OracleParameter("P_CURSOR", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            DataTable dt = _db.ExecuteQuery("SP_GET_USER_MENU", parameters);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MenuModel
                {
                    Id = row["ID"].ToString(),
                    Name = row["MENUNAME"].ToString(),
                    Controller = row["CONTROLLERNAME"]?.ToString(),
                    Action = row["ACTIONNAME"]?.ToString(),
                    Icon = row["ICON"]?.ToString(),
                    ParentId = row["PARENTID"]?.ToString()
                });
            }

            return list;
        }
        
    }
}
