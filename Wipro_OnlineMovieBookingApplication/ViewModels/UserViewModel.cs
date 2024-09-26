using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.ViewModels
{
    public class UserViewModel
    {
        //[HiddenInput]
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
        //public int BookingId { get; set; }

        public List<BookingViewModel> Bookings { get; set; } = new List<BookingViewModel>();
    }
}
