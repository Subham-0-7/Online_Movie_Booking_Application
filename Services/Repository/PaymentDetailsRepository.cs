using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly OnlineMovieBookingApplicationContext paymentDbContext;
        public PaymentDetailsRepository(OnlineMovieBookingApplicationContext _paymentDbContext)
        {
            paymentDbContext = _paymentDbContext;
        }

        public int AddPaymentDetails(PaymentDetail paymentDetail)
        {
            paymentDbContext.PaymentDetails.Add(paymentDetail);
            return paymentDbContext.SaveChanges();
        }

        //public bool DeletePaymentDetails(int id)
        //{
        //    var filterData = paymentDbContext.PaymentDetails.SingleOrDefault(p => p.PaymentId == id);
        //    var result = paymentDbContext.PaymentDetails.Remove(filterData);
        //    paymentDbContext.SaveChanges();
        //    return result != null ? true : false;
        //}

        public IEnumerable<PaymentDetail> GetAllPaymentDetails()
        {
            return paymentDbContext.PaymentDetails
                .ToList();
        }

        public PaymentDetail GetPaymentDetailsById(int id)
        {
            return paymentDbContext.PaymentDetails
                .SingleOrDefault(p => p.PaymentId == id);
        }

        public IEnumerable<PaymentDetail> GetAllPaymentDetailsForUser(int userId)
        {
            return paymentDbContext.PaymentDetails.Where(p => p.UserId == userId).ToList();
        }

        //public int UpdatePaymentDetails(PaymentDetail paymentDetail)
        //{
        //    paymentDbContext.PaymentDetails.Update(paymentDetail);
        //    return paymentDbContext.SaveChanges();
        //}

        //public int ConfirmPayment(PaymentDetail paymentDetail)
        //{
        //    paymentDbContext.PaymentDetails.Update(paymentDetail);
        //    return paymentDbContext.SaveChanges();
        //}
    }
}
