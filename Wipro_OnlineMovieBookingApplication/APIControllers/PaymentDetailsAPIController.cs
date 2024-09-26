using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Repository;
using Wipro_OnlineMovieBookingApplication.DTOs;
using Wipro_OnlineMovieBookingApplication.ViewModels;

namespace Wipro_OnlineMovieBookingApplication.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsAPIController : ControllerBase
    {
        private readonly IPaymentDetailsRepository paymentDetailsRepository;
        private readonly OnlineMovieBookingApplicationContext context;
        IMapper mapper;
        public PaymentDetailsAPIController(IPaymentDetailsRepository _paymentDetailsRepository,
            OnlineMovieBookingApplicationContext _context, IMapper _mapper)
        {
            paymentDetailsRepository = _paymentDetailsRepository;
            context = _context;
            mapper = _mapper;
        }

        [HttpGet("GetAllPayments")]
        public ActionResult<IEnumerable<PaymentDetailsDTO>> GetAllPaymentDetails()
        {
            List<PaymentDetailsDTO> lstPayments = new List<PaymentDetailsDTO>();
            var paymentDetails = paymentDetailsRepository.GetAllPaymentDetails().ToList();


            foreach (var payment in paymentDetails)
            {
                payment.User = context.Users.SingleOrDefault(u => u.UserId == payment.UserId);
                payment.Movie = context.Movies.SingleOrDefault(u => u.MovieId == payment.MovieId);

                PaymentDetailsDTO payments = new PaymentDetailsDTO();
                payments.PaymentId = payment.PaymentId;
                payments.TransactionId = payment.TransactionId;
                payments.UserId = payment.UserId;
                payments.UserName = payment.User.UserName;
                payments.Email = payment.User.Email;
                payments.MovieId = payment.MovieId;
                payments.MovieName = payment.Movie.MovieName;
                payments.Amount = payment.Amount;
                payments.PaymentDate = payment.PaymentDate;
                payments.IsConfirmed = payment.IsConfirmed;

                lstPayments.Add(payments);
            }
            return Ok(lstPayments);
        }

        [HttpGet("GetPaymentDetailsById")]
        public ActionResult<PaymentDetailsDTO> GetPaymentDetailsById(int id)
        {
            var paymentDetail = paymentDetailsRepository.GetPaymentDetailsById(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            paymentDetail.User = context.Users.SingleOrDefault(u => u.UserId == paymentDetail.UserId);
            paymentDetail.Movie = context.Movies.SingleOrDefault(u => u.MovieId == paymentDetail.MovieId);

            PaymentDetailsDTO paymentDetailsDTO = new PaymentDetailsDTO();
            paymentDetailsDTO.PaymentId = paymentDetail.PaymentId;
            paymentDetailsDTO.TransactionId = paymentDetail.TransactionId;
            paymentDetailsDTO.UserId = paymentDetail.UserId;
            paymentDetailsDTO.UserName = paymentDetail.User.UserName;
            paymentDetailsDTO.Email = paymentDetail.User.Email;
            paymentDetailsDTO.IsAdmin = paymentDetail.User.IsAdmin;
            paymentDetailsDTO.MovieId = paymentDetail.MovieId;
            paymentDetailsDTO.MovieName = paymentDetail.Movie.MovieName;
            paymentDetailsDTO.Amount = paymentDetail.Amount;
            paymentDetailsDTO.PaymentDate = paymentDetail.PaymentDate;
            paymentDetailsDTO.IsConfirmed = paymentDetail.IsConfirmed;

            return Ok(paymentDetailsDTO);
        }

        [HttpGet("GetAllPaymentDetailsForUser")]
        public ActionResult GetAllPaymentDetailsForUser(int userId)
        {
            List<PaymentDetailsDTO> lstUserPayments = new List<PaymentDetailsDTO>();
            var paymentDetails = paymentDetailsRepository.GetAllPaymentDetailsForUser(userId);

            foreach (var payment in paymentDetails)
            {
                payment.User = context.Users.SingleOrDefault(u => u.UserId == payment.UserId);
                payment.Movie = context.Movies.SingleOrDefault(u => u.MovieId == payment.MovieId);

                PaymentDetailsDTO payments = new PaymentDetailsDTO();
                payments.PaymentId = payment.PaymentId;
                payments.TransactionId = payment.TransactionId;
                payments.UserId = payment.UserId;
                payments.UserName = payment.User.UserName;
                payments.Email = payment.User.Email;
                payments.MovieId = payment.MovieId;
                payments.MovieName = payment.Movie.MovieName;
                payments.Amount = payment.Amount;
                payments.PaymentDate = payment.PaymentDate;
                payments.IsConfirmed = payment.IsConfirmed;

                lstUserPayments.Add(payments);
            }
            return Ok(lstUserPayments);
        }

        //[HttpPut("ConfirmPayment")]
        //public ActionResult ConfirmPayment([FromBody] PaymentDetail model)
        //{
        //    var paymentDetail = context.PaymentDetails
        //    .SingleOrDefault(p => p.PaymentId == model.PaymentId);

        //    paymentDetail.IsConfirmed = true;
        //    context.PaymentDetails.Update(paymentDetail);
        //    context.SaveChanges();
        //    return Ok(paymentDetail);
        //}
    }
}
