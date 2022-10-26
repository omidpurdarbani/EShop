using System.Security.Claims;
using System.Runtime.Intrinsics.X86;
using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Models;
using Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // if (_userRepository.IsUserExistByEmail(model.Email.ToLower()))
            // {
            //     ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام شده است");
            //     return View(model);
            // }

            User adduser = new User()
            {
                Email = model.Email.ToLower(),
                Name = model.Name,
                Password = model.Password,
                IsAdmin = false,
                RegisterTime = DateTime.Now
            };
            _userRepository.AddUser(adduser);

             var loginmodel= new LoginViewModel()
            {
                Email=model.Email.ToLower(),
                Password=model.Password,
                RememberMe=true
            };

            var user = _userRepository.GetUserToLogin(model.Email.ToLower(), model.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim("Admin",user.IsAdmin.ToString())
            };
 
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties{
                IsPersistent=loginmodel.RememberMe
            };

            HttpContext.SignInAsync(principal,properties);
            
            return View("/Views/Account/SuccessRegister.cshtml", model);
        }

        public IActionResult VerifyEmail(String email)
        {
            if (_userRepository.IsUserExistByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }
            return Json(true);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userRepository.GetUserToLogin(model.Email.ToLower(), model.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim("Admin",user.IsAdmin.ToString())
            };
 
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties{
                IsPersistent=model.RememberMe
            };

            HttpContext.SignInAsync(principal,properties);
            
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

        #endregion
    }
}
