using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly OnlineMovieBookingApplicationContext movieDbContext;
        public MovieRepository(OnlineMovieBookingApplicationContext _movieDbContext)
        {
            movieDbContext = _movieDbContext;
        }

        public int AddMovie(Movie movie)
        {
            movieDbContext.Movies.Add(movie);
            return movieDbContext.SaveChanges();
        }

        public bool DeleteMovie(int id)
        {
            var filterData = movieDbContext.Movies.SingleOrDefault(m => m.MovieId == id);
            var result = movieDbContext.Movies.Remove(filterData);
            movieDbContext.SaveChanges();
            return result != null ? true : false;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return movieDbContext.Movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            return movieDbContext.Movies.SingleOrDefault(m => m.MovieId == id);
        }

        public bool MovieExists(int movieId)
        {
            return movieDbContext.Movies.Any(m => m.MovieId == movieId);
        }

        public int UpdateMovie(Movie movie)
        {
            movieDbContext.Movies.Update(movie);
            return movieDbContext.SaveChanges();
        }
    }
}
