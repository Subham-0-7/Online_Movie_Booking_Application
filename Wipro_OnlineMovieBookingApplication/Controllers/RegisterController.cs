using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.Controllers
{
    public class RegisterController : Controller
    {
        HttpClient client;
        IConfiguration configuration;
        IMapper mapper;
        public RegisterController(IConfiguration _configuration, IMapper _mapper)
        {
            mapper = _mapper;
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult RegisterUserSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel userModel)
        {
            string url = "api/RegisterAPI/Register";
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync(url, userModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegisterUserSuccess");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(userModel);
        }
    }
}
