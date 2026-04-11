using POS_ASP_ORA.Models;
using System;
using System.Collections.Generic;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IUserService
    {
        List<Users> GetUsers();

        string InsertUser(Users model);

        string UpdateUser(Users model);

        string DeleteUser(Guid id);
    }
}