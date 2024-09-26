using Domain.Models;

namespace Wipro_OnlineMovieBookingApplication.DTOs
{
    public class BookingDTOCreate
    {
        //public long BookingId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int MoviePrice { get; set; }
        public DateOnly BookingDate { get; set; }
        public string ShowTime { get; set; }
        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }
    }
}
