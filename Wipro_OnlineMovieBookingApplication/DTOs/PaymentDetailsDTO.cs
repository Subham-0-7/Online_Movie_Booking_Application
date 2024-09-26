using Domain.Models;

namespace Wipro_OnlineMovieBookingApplication.DTOs
{
    public class PaymentDetailsDTO
    {
        public int PaymentId { get; set; }

        public Guid TransactionId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email {  get; set; }

        public int IsAdmin { get; set; }

        public int MovieId { get; set; } 

        public string MovieName { get; set; }

        public int BookingId { get; set; }

        public int Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsConfirmed { get; set; }

        public virtual Booking Booking { get; set; } = null!;

        public virtual Movie Movie { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
