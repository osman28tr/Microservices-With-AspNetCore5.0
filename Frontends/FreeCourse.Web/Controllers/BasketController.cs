﻿using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        public BasketController(ICatalogService catalogService,IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }
        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetCourseById(courseId);

            var basketItem = new BasketItemViewModel { CourseId = courseId, CourseName = course.Name, Price = course.Price };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);

            return RedirectToAction(nameof(Index));
        }
    }
}
