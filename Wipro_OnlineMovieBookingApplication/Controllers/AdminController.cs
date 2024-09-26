using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.Controllers
{
    public class AdminController : Controller
    {
        HttpClient client;
        IConfiguration configuration;
        IMapper mapper;
        public AdminController(IConfiguration _configuration, IMapper _mapper)
        {
            mapper = _mapper;
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> UserDetails()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            HttpResponseMessage res = await client.GetAsync("api/AdminAPI/ListUsers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<UserViewModel>>(result);
            }
            return View(model);
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel userModel)
        {
            string url = "api/AdminAPI/AddUser";
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync(url, userModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AddUserSuccess", "SuccessPopUp");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            UserViewModel objUserModel = new UserViewModel();
            string url = "api/AdminAPI/SingleUser?userId=";
            using (var response = await client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                objUserModel = JsonConvert.DeserializeObject<UserViewModel>(result);
            }
            EditUserViewModel editUserViewModel = mapper.Map<EditUserViewModel>(objUserModel);
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel userEditModel)
        {
            string url = "api/AdminAPI/EditUser";
            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync(url, userEditModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EditUserByAdminSuccess", "SuccessPopUp");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(userEditModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            UserViewModel objUserModel = new UserViewModel();
            string url = "api/AdminAPI/SingleUser?userId=";
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            using (var response = await client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                objUserModel = JsonConvert.DeserializeObject<UserViewModel>(result, settings);
            }
            return View(objUserModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            UserViewModel objUserModel = new UserViewModel();
            string url = "api/AdminAPI/SingleUser?userId=";
            using (var response = await client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                objUserModel = JsonConvert.DeserializeObject<UserViewModel>(result);
            }
            return View(objUserModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            string url = "api/AdminAPI/DeleteUser?userId=";
            await client.DeleteAsync(url + id);

            return RedirectToAction("DeleteViewSuccess", "SuccessPopUp");
        }

        [HttpGet]
        public async Task<IActionResult> SetAdminUserList()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            HttpResponseMessage res = await client.GetAsync("api/AdminAPI/ListUsers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<UserViewModel>>(result);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SetAdmin(int userId)
        {
            string url = $"api/AdminAPI/SetAdmin?userId={userId}";

            var response = await client.PutAsJsonAsync(url, userId);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }

            return RedirectToAction("SetAdminSuccess", "SuccessPopUp");
        }


        //[HttpGet]
        //public ActionResult AddMovies()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddMovies(AddMovieViewModel movieModel)
        //{
        //    string url = "api/MovieAPI/AddMovies";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var response = await client.PostAsJsonAsync(url, movieModel);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction("AddMovieSuccess", "SuccessPopUp");
        //            }
        //            else
        //            {
        //                var errorContent = await response.Content.ReadAsStringAsync();
        //                ModelState.AddModelError(string.Empty, $"Server error: {response.StatusCode} - {errorContent}");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, $"Exception occurred: {ex.Message}");
        //        }
        //    }
        //    return View(movieModel);
        //}

        //[HttpGet]
        //public async Task<IActionResult> EditMovies(int? id)
        //{
        //    MovieViewModel movieViewModel = new MovieViewModel();
        //    string url = "api/MovieAPI/GetMovieById?movieId=";
        //    using (var response = await client.GetAsync(url + id))
        //    {
        //        var result = response.Content.ReadAsStringAsync().Result;
        //        movieViewModel = JsonConvert.DeserializeObject<MovieViewModel>(result);
        //    }
        //    EditMovieViewModel editMovieModel = mapper.Map<EditMovieViewModel>(movieViewModel);
        //    return View(editMovieModel);
        //}
        //[HttpPost]
        //public async Task<IActionResult> EditMovies(EditMovieViewModel movieEditModel)
        //{
        //    string url = "api/MovieAPI/EditMovies";
        //    if (ModelState.IsValid)
        //    {
        //        var response = await client.PutAsJsonAsync(url, movieEditModel);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("EditMovieSuccess", "SuccessPopUp");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Server error try after some time.");
        //        }
        //    }
        //    else
        //    {
        //        foreach (var modelState in ModelState.Values)
        //        {
        //            foreach (var error in modelState.Errors)
        //            {
        //                Console.WriteLine(error.ErrorMessage);
        //            }
        //        }
        //    }
        //    return View(movieEditModel);
        //}

        //[HttpGet]
        //public async Task<IActionResult> DeleteMovies(int? id)
        //{
        //    MovieViewModel movieViewModel = new MovieViewModel();
        //    string url = "api/MovieAPI/GetMovieById?movieId=";
        //    using (var response = await client.GetAsync(url + id))
        //    {
        //        var result = response.Content.ReadAsStringAsync().Result;
        //        movieViewModel = JsonConvert.DeserializeObject<MovieViewModel>(result);
        //    }
        //    return View(movieViewModel);
        //}
        //[HttpPost, ActionName("DeleteMovies")]
        //public async Task<IActionResult> DeleteConfirmMovies(int id)
        //{
        //    string url = "api/MovieAPI/DeleteMovie?movieId=";
        //    await client.DeleteAsync(url + id);

        //    return RedirectToAction("DeleteMovieSuccess", "SuccessPopUp");
        //}

        //[HttpGet]
        //public async Task<IActionResult> MovieDetails()
        //{
        //    List<MovieViewModel> model = new List<MovieViewModel>();
        //    HttpResponseMessage res = await client.GetAsync("api/MovieAPI/GetAllMovies");
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var result = res.Content.ReadAsStringAsync().Result;
        //        model = JsonConvert.DeserializeObject<List<MovieViewModel>>(result);
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddMovies(AddMovieViewModel movieModel)
        //{
        //    string url = "api/MovieAPI/AddMovies";
        //    if(ModelState.IsValid)
        //    {
        //        var response = await client.PostAsJsonAsync(url, movieModel);
        //        if(response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("AddMovieSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Server error try after some time.");
        //        }
        //    }
        //    return View(movieModel);
        //}

        //public IActionResult SelectGenre()
        //{
        //    var viewModel = new AddMovieViewModel()
        //    {
        //        Genres = new List<Genre>
        //        {
        //        new Genre {GenreId = 1, GenreName = "Action"},
        //        new Genre {GenreId = 2, GenreName = "Horror"},
        //        new Genre {GenreId = 3, GenreName = "Comedy"},
        //        new Genre {GenreId = 4, GenreName = "Thriller"},
        //        new Genre {GenreId = 5, GenreName = "Sci-Fi"},
        //        }
        //    };
        //    return View(viewModel);
        //}

    }
}
