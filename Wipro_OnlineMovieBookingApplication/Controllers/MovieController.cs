using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Repository;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.Controllers
{
    public class MovieController : Controller
    {
        HttpClient client;
        IConfiguration configuration;
        IMapper mapper;
        OnlineMovieBookingApplicationContext context;
        public MovieController(IConfiguration _configuration, IMapper _mapper,
            OnlineMovieBookingApplicationContext _context)
        {
            configuration = _configuration;
            mapper = _mapper;
            context = _context;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            
        }

        [HttpGet]
        public ActionResult AddMovies()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMovies(AddMovieViewModel movieModel)
        {
            string url = "api/MovieAPI/AddMovies";
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await client.PostAsJsonAsync(url, movieModel);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("AddMovieSuccess", "SuccessPopUp");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Server error: {response.StatusCode} - {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Exception occurred: {ex.Message}");
                }
            }
            return View(movieModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditMovies(int? id)
        {
            EditMovieViewModel movieViewModel = new EditMovieViewModel();
            string url = "api/MovieAPI/GetMovieById?movieId=";
            using (var response = await client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                movieViewModel = JsonConvert.DeserializeObject<EditMovieViewModel>(result);
            }
            //EditMovieViewModel editMovieModel = mapper.Map<EditMovieViewModel>(movieViewModel);
            return View(movieViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditMovies(EditMovieViewModel movieEditModel)
        {
            string url = "api/MovieAPI/EditMovies";
            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync(url, movieEditModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EditMovieSuccess", "SuccessPopUp");
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
            return View(movieEditModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMovies(int? id)
        {
            MovieViewModel movieViewModel = new MovieViewModel();
            string url = "api/MovieAPI/GetMovieById?movieId=";
            using (var response = await client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                movieViewModel = JsonConvert.DeserializeObject<MovieViewModel>(result);
            }
            return View(movieViewModel);
        }
        [HttpPost, ActionName("DeleteMovies")]
        public async Task<IActionResult> DeleteConfirmMovies(int id)
        {
            string url = "api/MovieAPI/DeleteMovie?movieId=";
            await client.DeleteAsync(url + id);

            return RedirectToAction("DeleteMovieSuccess", "SuccessPopUp");
        }

        [HttpGet]
        public async Task<IActionResult> MovieDetails()
        {
            List<MovieViewModel> model = new List<MovieViewModel>();
            HttpResponseMessage res = await client.GetAsync("api/MovieAPI/GetAllMovies");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<MovieViewModel>>(result);
            }
            return View(model);
        }


    }
}
