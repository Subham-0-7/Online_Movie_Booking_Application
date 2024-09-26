using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Wipro_OnlineMovieBookingApplication.DTOs;

namespace Wipro_OnlineMovieBookingApplication.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAPIController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;
        public MovieAPIController(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }

        [HttpPost("AddMovies")]
        public ActionResult AddMovies(MovieDTOCreate model)
        {
            Movie movieEntity = new Movie()
            {
                MovieName = model.MovieName,
                Synopsis = model.Synopsis,
                Director = model.Director,
                Duration = model.Duration,
                Rating = model.Rating,
                MovieImage = model.MovieImage,
                MoviePrice = model.MoviePrice,
                Genre = model.Genre
            };
            movieRepository.AddMovie(movieEntity);
            return Ok(movieEntity);
        }

        [HttpGet("GetMovieById")]
        public ActionResult GetMovieById(int movieId)
        {
            MovieDTO movieDTO = new MovieDTO();
            Movie movieEntity = movieRepository.GetMovie(movieId);
            movieDTO.MovieId = movieEntity.MovieId;
            movieDTO.MovieName = movieEntity.MovieName;
            movieDTO.Synopsis = movieEntity.Synopsis;
            movieDTO.Director = movieEntity.Director;
            movieDTO.Duration = movieEntity.Duration;
            movieDTO.Rating = movieEntity.Rating;
            movieDTO.MovieImage = movieEntity.MovieImage;
            movieDTO.MoviePrice = movieEntity.MoviePrice;
            movieDTO.Genre = movieEntity.Genre;

            return Ok(movieDTO);
        }

        [HttpGet("GetAllMovies")]
        public ActionResult GetAllMovies()
        {
            List<MovieDTO> lstMovies = new List<MovieDTO>();
            movieRepository.GetAllMovies().ToList().ForEach(m =>
            {
                MovieDTO movieDTO = new MovieDTO()
                {
                    MovieId = m.MovieId,
                    MovieName = m.MovieName,
                    Synopsis = m.Synopsis,
                    Director = m.Director,
                    Duration = m.Duration,
                    Rating = m.Rating,
                    MovieImage = m.MovieImage,
                    MoviePrice = m.MoviePrice,
                    Genre = m.Genre
                };
                lstMovies.Add(movieDTO);
            });
            return Ok(lstMovies);
        }

        [HttpPut("EditMovies")]
        public ActionResult EditMovies(MovieDTO model)
        {
            Movie movieEntity = new Movie()
            {
                MovieId = model.MovieId,
                MovieName = model.MovieName,
                Synopsis = model.Synopsis,
                Director = model.Director,
                Duration = model.Duration,
                Rating = model.Rating,
                MovieImage = model.MovieImage,
                MoviePrice = model.MoviePrice,
                Genre = model.Genre
            };
            movieRepository.UpdateMovie(movieEntity);
            return Ok(movieEntity);
        }

        [HttpDelete("DeleteMovie")]
        public ActionResult DeleteMovie(int movieId)
        {
            return Ok(movieRepository.DeleteMovie(movieId));
        }
    }
}
