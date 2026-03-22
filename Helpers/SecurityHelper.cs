using Newtonsoft.Json;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services;
using System.Security.Cryptography;
using System.Text;

namespace POS_ASP_ORA.Helpers
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }   
    }

}
