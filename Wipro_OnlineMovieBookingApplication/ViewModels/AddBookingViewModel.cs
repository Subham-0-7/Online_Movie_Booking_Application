using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.ViewModels
{
    public class AddBookingViewModel
    {
        [HiddenInput]
        public int BookingId { get; set; }
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        [Display(Name = "Movie Name")]
        public int MovieId { get; set; }
        public int MoviePrice { get; set; }
        public DateOnly BookingDate { get; set; }
        public string? ShowTime { get; set; }
        public int NumberOfTickets { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
        //public double TotalPrice { get; set; }
    }
}
