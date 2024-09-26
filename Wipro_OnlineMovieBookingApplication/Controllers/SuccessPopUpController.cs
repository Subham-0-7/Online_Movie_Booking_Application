using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace Wipro_OnlineMovieBookingApplication.Controllers
{
    public class SuccessPopUpController : Controller
    {
        HttpClient client;
        IConfiguration configuration;
        IUserRepository userRepository;
        public SuccessPopUpController(IConfiguration _configuration,
            IUserRepository _userRepository)
        {
            userRepository = _userRepository;
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult AddUserSuccess()
        {
            return View();
        }
        public ActionResult EditUserSuccess(int id)
        {
            var userModel = userRepository.GetUser(id);
            return View(userModel);
        }
        public ActionResult EditUserByAdminSuccess()
        {
            return View();
        }
        public ActionResult DeleteViewSuccess()
        {
            return View();
        }
        public ActionResult AddMovieSuccess()
        {
            return View();
        }
        public ActionResult EditMovieSuccess()
        {
            return View();
        }
        public ActionResult DeleteMovieSuccess()
        {
            return View();
        }
        public ActionResult LoginFailureAdmin()
        {
            return View();
        }
        public ActionResult LoginFailureUser()
        {
            return View();
        }
        public ActionResult SetAdminSuccess()
        {
            return View();
        }
    }
}
