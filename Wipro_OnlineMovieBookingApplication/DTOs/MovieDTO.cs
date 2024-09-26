using Domain.Models;

namespace Wipro_OnlineMovieBookingApplication.DTOs
{
    public class MovieDTO
    {
        public int MovieId { get; set; }

        public string? MovieName { get; set; }

        public string? Synopsis { get; set; }

        public string? Director { get; set; }

        public string? Duration { get; set; }

        public string? Genre { get; set; }

        public int? Rating { get; set; }

        public string? MovieImage { get; set; }

        public int MoviePrice { get; set; }

        //public virtual ICollection<Genre> Genres { get; set; }
    }
}
