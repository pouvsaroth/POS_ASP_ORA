using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace POS_ASP_ORA.Helpers
{
    public class OracleDbHelper
    {
        private readonly string _connectionString;

        public OracleDbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        // Execute Insert / Update / Delete
        public void ExecuteNonQuery(string procedureName, List<OracleParameter> parameters)
        {
            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Execute Select
        public DataTable ExecuteQuery(string procedureName, List<OracleParameter> parameters)
        {
            DataTable dt = new DataTable();

            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
