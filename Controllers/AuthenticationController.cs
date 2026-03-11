using Microsoft.AspNetCore.Mvc;
using POS_ASP_ORA.Services;

namespace POS_ASP_ORA.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var (result, userId, isActive, email) = _authService.Login(username, password);

                if (result == "SUCCESS")
                {
                    // Set user session or authentication logic
                    HttpContext.Session.SetString("UserId", userId);
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetString("Email", email);

                    return Json(new { success = true, message = "Login successful!" });
                }
                else if (result == "INACTIVE")
                {
                    return Json(new { success = false, message = "Your account is inactive." });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid username or password." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
