using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OnlineMovieBookingApplicationContext bookingDbContext;
        public BookingRepository(OnlineMovieBookingApplicationContext _bookingDbContext)
        {
            bookingDbContext = _bookingDbContext;
        }

        public int AddBookingDetails(Booking booking)
        {
            bookingDbContext.Bookings.Add(booking);
            return bookingDbContext.SaveChanges();
        }

        public bool DeleteBookingDetails(int id)
        {
            var filterData = bookingDbContext.Bookings.SingleOrDefault(b => b.BookingId == id);
            var result = bookingDbContext.Bookings.Remove(filterData);
            bookingDbContext.SaveChanges();
            return result != null ? true : false;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return bookingDbContext.Bookings.ToList();
        }

        public Booking GetBooking(int id)
        {
            return bookingDbContext.Bookings.SingleOrDefault(b => b.BookingId == id);
        }

        public int UpdateBookingDetails(Booking booking)
        {
            bookingDbContext.Bookings.Update(booking);
            return bookingDbContext.SaveChanges();
        }

        //public IEnumerable<Booking> GetAllBookingsByUserId(int userId)
        //{
        //    var bookings = (from booking in bookingDbContext.Bookings
        //                    join user in bookingDbContext.Users on booking.UserId equals user.UserId
        //                    join movie in bookingDbContext.Movies on booking.MovieId equals movie.MovieId
        //                    where booking.UserId == userId
        //                    select new Booking
        //                    {
        //                        BookingId = booking.BookingId,
        //                        UserId = booking.UserId,
        //                        UserName = booking.User.UserName,
        //                        MovieId = booking.MovieId,
        //                        MovieName = booking.Movie.MovieName,
        //                        MoviePrice = booking.Movie.MoviePrice,
        //                        BookingDate = booking.BookingDate,
        //                        ShowTime = booking.ShowTime,
        //                        NumberOfTickets = booking.NumberOfTickets,
        //                        TotalPrice = booking.NumberOfTickets * booking.Movie.MoviePrice
        //                    }).ToList();

        //    return bookings;
        //}
    }
}
