using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.ViewModels
{
    public class BookingViewModel
    {
        //[HiddenInput]
        public int BookingId { get; set; }
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int MoviePrice { get; set; }
        public DateOnly BookingDate { get; set; }
        public string? ShowTime { get; set; }
        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
    }

}
