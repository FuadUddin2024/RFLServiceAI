using QCMS.Repositories;
using QCMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace QCMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountsController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // =========================
        // LOGIN VIEW
        // =========================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // =========================
        // LOGIN POST
        // =========================
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string Username, string Password, string StaffID, string HRISPassword)
        {
            var user = _userRepository.GetUserInfo(Username, Password);

            if (user == null)
                return Json(new { success = false, message = "Invalid system credentials" });

            // =========================
            // SOFTWARE CHECK
            // =========================
            if (user.SOFT_ON == "N")
                return Json(new { success = false, message = user.INFO_WELCOME_MSG });


            if (string.IsNullOrEmpty(user.USERID) || string.IsNullOrEmpty(user.USERNAME))
                return Json(new { success = false, message = "User profile is incomplete. Contact MIS." });

            // =========================
            // SESSION STORAGE
            // =========================
            HttpContext.Session.SetString("USERID", user.USERID ?? "");
            HttpContext.Session.SetString("USERNAME", user.USERNAME ?? "");
            HttpContext.Session.SetString("DUSR_WHID", user.DUSR_WHID ?? "");
            HttpContext.Session.SetString("DIST_NAME", user.DIST_NAME ?? "");
            HttpContext.Session.SetString("DUSR_BUSN", user.DUSR_BUSN ?? "");

            // =========================
            // COOKIE AUTHENTICATION
            // =========================
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.USERNAME ?? ""),
                new Claim("USERID", user.USERID ?? ""),
                new Claim("WHID", user.DUSR_WHID ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // keep login across sessions
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });

            return Json(new { success = true, redirect = Url.Action("Index", "Home") });
        }

        // =========================
        // LOGOUT
        // =========================
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // =========================
        // ACCESS DENIED
        // =========================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
