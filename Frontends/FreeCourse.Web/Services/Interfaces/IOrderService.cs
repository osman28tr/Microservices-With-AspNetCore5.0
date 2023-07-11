using FreeCourse.Web.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        //<summary>
        //  senkron iletişim, order mikroservisine istek yapulır.
        //</summary>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        //<summary>
        //  asenkron iletişim, sipariş bilgileri rabbitMQ'ya gönderilecek.
        //</summary>
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();
    }
}
