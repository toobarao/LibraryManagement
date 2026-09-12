using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                ViewBag.ErrorMessage = null;
                if (ModelState.IsValid)
                {
                   bool result =   accountService.Login(loginViewModel);
                    if (result)
                    {
                        var claims = new List<Claim>
                        {
                           
                            new Claim(ClaimTypes.Name, loginViewModel.Name),
                            new Claim(ClaimTypes.Role, loginViewModel.UserType.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        switch (loginViewModel.UserType)
                        {
                            case UserType.Librarian:
                                return RedirectToAction("Index", "Librarian");
                            case UserType.PremiumMember:
                                return RedirectToAction("Index", "Member");
                            default:
                                return RedirectToAction("Index", "Member");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", model);
            }
           
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                ViewBag.ErrorMessage = null;
            if (ModelState.IsValid)
            {
                bool isRegistered = accountService.Register(registerViewModel);
                if (isRegistered)
                {
                    return View("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "User already exists";
                }
            }
            return View();
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
     
            return RedirectToAction("Login","Account");
        }

        public IActionResult ErrorPage()
        {
            ErrorModel errorModel = new ErrorModel();
            errorModel.ErrorMsg = "Page not found";
            errorModel.statusCode = 404;
            return View("Error", errorModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorModel errorModel)
        {

            return View(errorModel);
            
        }

    }
}
