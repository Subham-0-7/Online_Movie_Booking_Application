using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IPaymentDetailsRepository
    {
        IEnumerable<PaymentDetail> GetAllPaymentDetails();
        PaymentDetail GetPaymentDetailsById(int id);
        int AddPaymentDetails(PaymentDetail paymentDetail);

        IEnumerable<PaymentDetail> GetAllPaymentDetailsForUser(int userId);
        //int ConfirmPayment(PaymentDetail paymentDetail);
    }
}
