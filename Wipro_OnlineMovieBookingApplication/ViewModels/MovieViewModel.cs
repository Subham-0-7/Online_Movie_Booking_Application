using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public string? MovieName { get; set; }

        public string? Synopsis { get; set; }

        public string? Director { get; set; }

        [Required]
        [Display(Name = "Duration (in minutes)")]
        public string? Duration { get; set; }

        public string? Genre { get; set; }

        public int? Rating { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required]
        public string? MovieImage { get; set; }
        [Display(Name = "Ticket Price")]
        public int MoviePrice { get; set; }

        //public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        //{
        //    new Genre {GenreId = 1, GenreName = "Action"},
        //    new Genre {GenreId = 2, GenreName = "Horror"},
        //    new Genre {GenreId = 3, GenreName = "Comedy"},
        //    new Genre {GenreId = 4, GenreName = "Thriller"},
        //    new Genre {GenreId = 5, GenreName = "Sci-Fi"},
        //};
    }
}
