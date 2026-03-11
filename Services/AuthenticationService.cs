using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using System.Data;

namespace POS_ASP_ORA.Services
{
    public class AuthenticationService
    {
        private readonly OracleDbHelper _db;

        public AuthenticationService(OracleDbHelper db)
        {
            _db = db;
        }

        public (string Result, string UserId, int IsActive, string Email) Login(string username, string password)
        {
            try
            {
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("P_USERNAME", OracleDbType.Varchar2) { Value = username },
                    new OracleParameter("P_PASSWORD", OracleDbType.Varchar2) { Value = password },
                    new OracleParameter("P_USER_ID", OracleDbType.Varchar2, 50) { Direction = ParameterDirection.Output },
                    new OracleParameter("P_IS_ACTIVE", OracleDbType.Int32) { Direction = ParameterDirection.Output },
                    new OracleParameter("P_EMAIL", OracleDbType.Varchar2, 100) { Direction = ParameterDirection.Output },
                    new OracleParameter("P_RESULT", OracleDbType.Varchar2, 50) { Direction = ParameterDirection.Output }
                };

                _db.ExecuteNonQuery("SP_USER_LOGIN", parameters);

                var result = parameters[5].Value.ToString();
                var userId = parameters[2].Value?.ToString();
                var isActive = Convert.ToInt32(parameters[3].Value);
                var email = parameters[4].Value?.ToString();

                return (result, userId, isActive, email);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in: " + ex.Message);
            }
        }
    }
}
