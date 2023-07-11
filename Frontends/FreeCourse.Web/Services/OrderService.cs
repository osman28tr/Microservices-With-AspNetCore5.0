using FreeCourse.Web.Models.Order;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOrder()
        {
            throw new System.NotImplementedException();
        }

        public Task Suspend(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
