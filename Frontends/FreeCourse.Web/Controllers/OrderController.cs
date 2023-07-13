using FreeCourse.Web.Models.Order;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //senkron iletişim:
            var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            //asenkron iletişim:
            //var orderSuspend = _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderStatus.IsSuccessful)
            {
				var basket = await _basketService.Get();
				ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return View();
            }
            //senkron iletişim:
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });

            //asenkron iletişim:
            //return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(0,1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            
            return View();
        }
    }
}
