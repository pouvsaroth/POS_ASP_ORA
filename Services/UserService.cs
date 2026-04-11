using Oracle.ManagedDataAccess.Client;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Data;

public class UserService : IUserService
{
    private readonly OracleDbHelper _db;

    public UserService(OracleDbHelper db)
    {
        _db = db;
    }

    public List<Users> GetUsers()
    {
        List<Users> list = new();

        var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","GET"),
            new OracleParameter("P_ID", DBNull.Value),
            new OracleParameter("P_USERNAME", DBNull.Value),
            new OracleParameter("P_PASSWORD", DBNull.Value),
            new OracleParameter("P_EMAIL", DBNull.Value),
            new OracleParameter("P_IS_ACTIVE", DBNull.Value),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor){Direction = ParameterDirection.Output}
        };

        DataTable dt = _db.ExecuteQuery("SP_USER_CRUD", parameters);

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Users
            {
                Id = new Guid((byte[])row["ID"]),
                Username = row["USERNAME"].ToString(),
                Password = row["PASSWORD"].ToString(),
                Email = row["EMAIL"].ToString(),
                IsActive = Convert.ToInt32(row["IS_ACTIVE"]) == 1,
                CreatedAt = Convert.ToDateTime(row["CREATED_AT"]),
                UpdatedAt = row["UPDATED_AT"] == DBNull.Value
                            ? null
                            : (DateTime?)Convert.ToDateTime(row["UPDATED_AT"])
            });
        }

        return list;
    }

    public string InsertUser(Users model)
    {
        var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","INSERT"),
            new OracleParameter("P_ID", DBNull.Value),
            new OracleParameter("P_USERNAME", model.Username),
            new OracleParameter("P_PASSWORD", model.Password),
            new OracleParameter("P_EMAIL", model.Email),
            new OracleParameter("P_IS_ACTIVE", model.IsActive ? 1 : 0),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor){Direction = ParameterDirection.Output}
        };

        _db.ExecuteNonQuery("SP_USER_CRUD", parameters);

        return "User Created";
    }

    public string UpdateUser(Users model)
    {
        var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","UPDATE"),
            new OracleParameter("P_ID", model.Id.ToByteArray()),
            new OracleParameter("P_USERNAME", model.Username),
            new OracleParameter("P_PASSWORD",
                string.IsNullOrEmpty(model.Password) ? DBNull.Value : model.Password),
            new OracleParameter("P_EMAIL", model.Email),
            new OracleParameter("P_IS_ACTIVE", model.IsActive ? 1 : 0),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor){Direction = ParameterDirection.Output}
        };

        _db.ExecuteNonQuery("SP_USER_CRUD", parameters);

        return "User Updated";
    }

    public string DeleteUser(Guid id)
    {
        var parameters = new List<OracleParameter>
        {
            new OracleParameter("P_ACTION","DELETE"),
            new OracleParameter("P_ID", id.ToByteArray()),
            new OracleParameter("P_USERNAME", DBNull.Value),
            new OracleParameter("P_PASSWORD", DBNull.Value),
            new OracleParameter("P_EMAIL", DBNull.Value),
            new OracleParameter("P_IS_ACTIVE", DBNull.Value),
            new OracleParameter("P_CURSOR", OracleDbType.RefCursor){Direction = ParameterDirection.Output}
        };

        _db.ExecuteNonQuery("SP_USER_CRUD", parameters);

        return "User Deleted";
    }
}