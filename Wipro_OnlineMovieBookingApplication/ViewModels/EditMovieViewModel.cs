using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.ViewModels
{
    public class EditMovieViewModel
    {
        public int MovieId { get; set; }

        public string? MovieName { get; set; }

        public string? Synopsis { get; set; }

        public string? Director { get; set; }

        [Required]
        [Display(Name = "Duration (in minutes)")]
        public string? Duration { get; set; }

        public int? Rating { get; set; }
        public string? Genre { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required]
        public string? MovieImage { get; set; }

        public int MoviePrice { get; set; }
    }
}
