using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            
        }
    }
}
