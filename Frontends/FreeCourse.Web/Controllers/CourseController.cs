using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
	[Authorize]
	public class CourseController : Controller
	{
		private readonly ICatalogService _catalogService;
		private readonly ISharedIdentityService _sharedIdentityService;
		public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
		{
			_catalogService = catalogService;
			_sharedIdentityService = sharedIdentityService;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
		}
		public async Task<IActionResult> Create()
		{
			var categories = await _catalogService.GetAllCategoryAsync();
			ViewBag.categoryList = new SelectList(categories, "Id", "Name");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateCourseInput createCourseInput)
		{
			var categories = await _catalogService.GetAllCategoryAsync();
			ViewBag.categoryList = new SelectList(categories, "Id", "Name");
			if (!ModelState.IsValid)
			{
				return View();
			}
			createCourseInput.UserId = _sharedIdentityService.GetUserId;
			await _catalogService.CreateCourseAsync(createCourseInput);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(string id)
		{
			var course = await _catalogService.GetCourseById(id);
			var categories = await _catalogService.GetAllCategoryAsync();
			
			if (course == null)
			{
				return RedirectToAction(nameof(Index));
			}
			ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
			UpdateCourseInput updateCourseInput = new()
			{
				Id = course.Id,
				Name = course.Name,
				Description = course.Description,
				Price = course.Price,
				Feature = course.Feature,
				CategoryId = course.CategoryId,
				UserId = course.UserId,
				Picture = course.Picture
			};
			return View(updateCourseInput);
		}
		[HttpPost]
		public async Task<IActionResult> Update(UpdateCourseInput updateCourseInput)
		{
			var categories = await _catalogService.GetAllCategoryAsync();
			ViewBag.categoryList = new SelectList(categories, "Id", "Name", updateCourseInput.Id);
			if (!ModelState.IsValid) { return View(); }

			await _catalogService.UpdateCourseAsync(updateCourseInput);
			return RedirectToAction(nameof(Index));
		}
	}
}
