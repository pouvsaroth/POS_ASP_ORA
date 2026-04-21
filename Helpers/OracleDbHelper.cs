using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace POS_ASP_ORA.Helpers
{
    public class OracleDbHelper
    {
        private readonly string _connectionString;

        private OracleConnection _connection;
        private OracleTransaction _transaction;

        public OracleDbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        // =========================
        // 🔥 BEGIN TRANSACTION
        // =========================
        public void BeginTransaction()
        {
            _connection = new OracleConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        // =========================
        // 🔥 COMMIT
        // =========================
        public void Commit()
        {
            _transaction?.Commit();
            _connection?.Close();
            _transaction = null;
            _connection = null;
        }

        // =========================
        // 🔥 ROLLBACK
        // =========================
        public void Rollback()
        {
            _transaction?.Rollback();
            _connection?.Close();
            _transaction = null;
            _connection = null;
        }

        // =========================
        // EXECUTE NON QUERY
        // =========================
        public void ExecuteNonQuery(string procedureName, List<OracleParameter> parameters)
        {
            // 🔥 Use existing connection if transaction is active
            if (_connection != null)
            {
                using (OracleCommand cmd = new OracleCommand(procedureName, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.Transaction = _transaction;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                // ✅ fallback (no transaction)
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
        }

        // =========================
        // EXECUTE QUERY
        // =========================
        public DataTable ExecuteQuery(string procedureName, List<OracleParameter> parameters)
        {
            DataTable dt = new DataTable();

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

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}