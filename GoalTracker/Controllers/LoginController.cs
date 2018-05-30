using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GoalTracker.ViewModels;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace GoalTracker.Controllers
{
    public class LoginController : Controller
    {
        UserLogic uLogic = new UserLogic();

        private bool CheckIfLoggedIn()
        {
            try
            {
                User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault().Any();

                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public IActionResult Index()
        {
            if (CheckIfLoggedIn())
            {
                return RedirectToAction("Index", "Goals");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            User user = uLogic.Login(model.Username, model.Password);

            if (user.Username == null)
            {
                ModelState.AddModelError("", "Username and/or password are incorrect.");

                return View(model);                    
            }

            CreateClaims(user);

            return RedirectToAction("Index", "Goals");            
        }

        private void CreateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Convert.ToString(user.Username)),
                new Claim("Id", Convert.ToString(user.UserId))
            };

            if (user.Role == UserRole.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Register()
        {
            if (CheckIfLoggedIn())
            {
                return RedirectToAction("Index", "Goals");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                Role = UserRole.User
            };

            // Probeert de user te registeren
            string eMessage = uLogic.Register(user);

            // Als iets NIET goed is gegaan bij de registratie, laat de error message zien.
            if (eMessage != "")
            {
                ModelState.AddModelError("", eMessage);

                return View(model);
            }

            // Als de registratie goed is gegaan, return de gebruiker terug naar de inlog pagina.
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Admin(FilterUsersViewModel model)
        {
            if (!String.IsNullOrEmpty(model.FilterString))
            {
                model.Users = uLogic.GetUsers(model.FilterString);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(int UserId)
        {
            User user = uLogic.GetUser(UserId);

            var model = new EditUserViewModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                Username = user.Username,
                OldPassword = user.Password,
                Role = user.Role,
                Banning = user.Banning
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel model)
        {
            var user = new User()
            {
                UserId = model.UserId,
                Email = model.Email,
                Username = model.Username,
                Role = model.Role,
                Banning = model.Banning
            };

            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = model.NewPassword;
            }
            else
            {
                user.Password = model.OldPassword;
            }

            bool success = uLogic.EditUser(user);

            if (user.Username == null)
            {
                ModelState.AddModelError("", "Editing failed.");

                return View(model);
            }

            return RedirectToAction("Admin", new { UserId = model.UserId });
        }
    }
}