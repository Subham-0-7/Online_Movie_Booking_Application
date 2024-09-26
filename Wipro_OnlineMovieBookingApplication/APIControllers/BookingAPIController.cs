using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using Wipro_OnlineMovieBookingApplication.DTOs;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingAPIController : ControllerBase
    {
        private readonly IBookingRepository iBookingRepository;
        //private readonly IUserRepository iUserRepository;
        //private readonly IMovieRepository iMovieRepository;
        //private readonly IPaymentDetailsRepository iPaymentRepository;
        private readonly OnlineMovieBookingApplicationContext context;
        public BookingAPIController(IBookingRepository _iBookingRepository, 
            OnlineMovieBookingApplicationContext _context)
        {
            iBookingRepository = _iBookingRepository;
            context = _context;
        }

        [HttpGet("GetAllBookings")]
        public ActionResult GetAllBookings()
        {
            var bookings = (from booking in context.Bookings
                            join user in context.Users on booking.UserId equals user.UserId
                            join movie in context.Movies on booking.MovieId equals movie.MovieId
                            select new BookingDTO
                            {
                                BookingId = booking.BookingId,
                                UserId = booking.UserId,
                                UserName = user.UserName,
                                Email = user.Email,
                                MovieId = booking.MovieId,
                                MovieName = movie.MovieName,
                                MoviePrice = movie.MoviePrice,
                                BookingDate = booking.BookingDate,
                                ShowTime = booking.ShowTime,
                                NumberOfTickets = booking.NumberOfTickets,
                                TotalPrice = booking.NumberOfTickets * booking.Movie.MoviePrice
                            }).ToList();

            return Ok(bookings);
        }

        [HttpGet("GetBookingById")]
        public ActionResult GetBookingById(int bookingId)
        {
            var booking = (from bookings in context.Bookings
                           where bookings.BookingId == bookingId
                           join user in context.Users on bookings.UserId equals user.UserId
                           join movie in context.Movies on bookings.MovieId equals movie.MovieId
                           select new BookingDTO
                           {
                               BookingId = bookings.BookingId,
                               UserId = bookings.UserId,
                               UserName = bookings.User.UserName,
                               Email = bookings.User.Email,
                               MovieId = bookings.MovieId,
                               MovieName = bookings.Movie.MovieName,
                               MoviePrice = bookings.Movie.MoviePrice,
                               BookingDate = bookings.BookingDate,
                               ShowTime = bookings.ShowTime,
                               NumberOfTickets = bookings.NumberOfTickets,
                               TotalPrice = bookings.NumberOfTickets * bookings.Movie.MoviePrice
                           }).FirstOrDefault();
            return Ok(booking);
        }

        [HttpPost("AddBooking")]
        public ActionResult AddBooking(BookingDTOCreate bookingModel)
        {
            Booking bookingEntity = new Booking()
            {
                UserId = bookingModel.UserId,
                MovieId = bookingModel.MovieId,
                MoviePrice = bookingModel.MoviePrice,
                BookingDate = bookingModel.BookingDate,
                ShowTime = bookingModel.ShowTime,
                NumberOfTickets = bookingModel.NumberOfTickets,
                //TotalPrice = bookingModel.TotalPrice,

            };
            iBookingRepository.AddBookingDetails(bookingEntity);
            return Ok(bookingEntity);
        }

        [HttpPut("EditBooking")]
        public ActionResult EditBooking(BookingDTO bookingModel)
        {
            Booking bookingEntity = new Booking()
            {
                BookingDate = bookingModel.BookingDate,
                NumberOfTickets = bookingModel.NumberOfTickets,
                //TotalPrice = bookingModel.TotalPrice
            };
            iBookingRepository.UpdateBookingDetails(bookingEntity);
            return Ok(bookingEntity);
        }

        [HttpDelete("DeleteBooking")]
        public ActionResult DeleteBooking(int bookingId)
        {
            return Ok(iBookingRepository.DeleteBookingDetails(bookingId));
        }

        [HttpGet("GetAllBookingForUser")]
        public ActionResult GetAllBookingsForUser(int userId)
        {
            var bookings = (from booking in context.Bookings
                            join user in context.Users on booking.UserId equals user.UserId
                            join movie in context.Movies on booking.MovieId equals movie.MovieId
                            where booking.UserId == userId
                            select new BookingDTO
                            {
                                BookingId = booking.BookingId,
                                UserId = booking.UserId,
                                UserName = booking.User.UserName,
                                Email = booking.User.Email,
                                MovieId = booking.MovieId,
                                MovieName = booking.Movie.MovieName,
                                MoviePrice = booking.Movie.MoviePrice,
                                BookingDate = booking.BookingDate,
                                ShowTime = booking.ShowTime,
                                NumberOfTickets = booking.NumberOfTickets,
                                TotalPrice = booking.NumberOfTickets * booking.Movie.MoviePrice
                            }).ToList();
            return Ok(bookings);
        }

    }
}
