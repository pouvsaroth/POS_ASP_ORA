using POS_ASP_ORA.Models;
using System.Collections.Generic;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IAuthService
    {
        (string Result, string UserId, int IsActive, string Email) Login(string username, string password);

        string RegisterUser(Users user);

        List<MenuModel> GetUserMenu(string userId);
    }
}