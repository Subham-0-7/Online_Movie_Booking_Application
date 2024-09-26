using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.Controllers
{
    public class LoginController : Controller
    {
        HttpClient client;
        IConfiguration configuration;

        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult LoginCategory()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AdminLogin(UserLoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                List<UserLoginViewModel> lstUsers = new List<UserLoginViewModel>();
                HttpResponseMessage res = await client.GetAsync("api/LoginAPI/CheckListOfUsers");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    lstUsers = JsonConvert.DeserializeObject<List<UserLoginViewModel>>(result);

                    var Data = lstUsers.FirstOrDefault(u => u.Email == loginModel.Email);
                    if (Data != null)
                    {
                        bool isValid = Data.Email == loginModel.Email && Data.Password == loginModel.Password;
                        if (isValid)
                        {
                            var userClaims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Email, Data.Email),
                                new Claim(ClaimTypes.Role, Data.IsAdmin == 1 ? "Admin" : "User"),
                                new Claim("UserId", Data.Id.ToString())
                            };

                            
                            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                            
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity));

                            
                            if (Data.IsAdmin == 1)
                                return RedirectToAction("AdminDashboard", "Admin");
                            else
                                return RedirectToAction("LoginFailureUser", "SuccessPopUp");
                        }
                        else
                        {
                            TempData["errorPassword"] = "Invalid Password";
                            return View(loginModel);
                        }
                    }
                    else
                    {
                        TempData["errorUsername"] = "User not found";
                        return View(loginModel);
                    }
                }
                else
                {
                    TempData["errorApi"] = "Api not found";
                    return View(loginModel);
                }

            }
            else
            {
                return View(loginModel);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> UserLogin(UserLoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                List<UserLoginViewModel> lstUsers = new List<UserLoginViewModel>();
                HttpResponseMessage res = await client.GetAsync("api/LoginAPI/CheckListOfUsers");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    lstUsers = JsonConvert.DeserializeObject<List<UserLoginViewModel>>(result);

                    var Data = lstUsers.FirstOrDefault(u => u.Email == loginModel.Email);
                    if (Data != null)
                    {
                        bool isValid = Data.Email == loginModel.Email && Data.Password == loginModel.Password;
                        if (isValid)
                        {
                            var userClaims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Email, Data.Email),
                                new Claim(ClaimTypes.Role, Data.IsAdmin == 1 ? "Admin" : "User"),
                                new Claim("UserId", Data.Id.ToString()) // Storing user ID as a claim
                            };

                            // Create identity with claims (based on role)
                            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                            // Sign in the user
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity));

                            // Redirect based on user type
                            if (Data.IsAdmin == 0)
                                return RedirectToAction("MovieDetails", "User", new { userId = Data.Id });
                            else
                                return RedirectToAction("LoginFailureAdmin", "SuccessPopUp");
                        }
                        else
                        {
                            TempData["errorPassword"] = "Invalid Password";
                            return View(loginModel);
                        }
                    }
                    else
                    {
                        TempData["errorUsername"] = "User not found";
                        return View(loginModel);
                    }
                }
                else
                {
                    TempData["errorApi"] = "Api not found";
                    return View(loginModel);
                }

            }
            else
            {
                return View(loginModel);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("LoginCategory", "Login");
        }
    }
}
