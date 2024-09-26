using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookings();
        Booking GetBooking(int id);
        int AddBookingDetails(Booking booking);
        int UpdateBookingDetails(Booking booking);
        bool DeleteBookingDetails(int id);

        //IEnumerable<Booking> GetAllBookingsByUserId(int userId);
    }
}
