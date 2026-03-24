using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POS_ASP_ORA.Helpers;
using POS_ASP_ORA.Models;
using POS_ASP_ORA.Services.Interfaces;
using System.Security.Claims;


namespace POS_ASP_ORA.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string hashedPassword = SecurityHelper.HashPassword(password);

            var (result, userId, isActive, email) = _authService.Login(username, hashedPassword.ToUpper());

            if (result == "SUCCESS")
            {
                
                var UserMenuList = _authService.GetUserMenu(userId);
                var menuTree = GeneralHelper.BuildMenuTree(UserMenuList);
                // HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(menuTree));
                var menuJson = JsonConvert.SerializeObject(menuTree);
                // ⚠️ Optional (recommended if menu is big)
                var menuBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(menuJson));
                // ✅ Create claims
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("UserId", userId),
                        new Claim(ClaimTypes.Email, email ?? ""),

                        // ✅ Store FULL menu in claim
                        new Claim("Menu", menuBase64)
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );

                return Json(new { success = true });
            }

            return Json(new { success = false, message = result });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all required fields.";
                return View(model);
            }

            // Hash the password before saving
            model.Password = SecurityHelper.HashPassword(model.Password);
            model.CreatedAt = DateTime.Now;
            model.IsActive = true; // or set as needed

            var result = _authService.RegisterUser(model);

            if (result == "SUCCESS")
            {
                //TempData["Success"] = "Registration successful! You can now log in.";
                return RedirectToAction("ViewPOSScreen", "POSScreen");
            }
            else
            {
                TempData["Error"] = result;
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("ViewPOSScreen", "POSScreen");
        }
    }
}
