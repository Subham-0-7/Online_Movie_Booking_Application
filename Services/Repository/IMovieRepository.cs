using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovie(int id);
        int AddMovie(Movie movie);
        int UpdateMovie(Movie movie);
        bool DeleteMovie(int id);
        bool MovieExists(int movieId);
    }
}
