using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("course", createCourseInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"course/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("category"); //sadece controller ismi verilmesi yeterli çünkü gerekli baseaddress  vs. yapılandırmalar startup tarafında yapıldı.
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data; //response class'ındaki data
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            //http:localhost:5000/services/catalog/courses
            var response = await _httpClient.GetAsync("course"); //sadece controller ismi verilmesi yeterli çünkü gerekli baseaddress  vs. yapılandırmalar startup tarafında yapıldı.
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data; //response class'ındaki data
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            //[controller]/GetAllByUserId/{userId}
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data; //response class'ındaki data
        }

        public async Task<CourseViewModel> GetCourseById(string courseId)
        {
            //[controller]/GetAllByUserId/{userId}
            var response = await _httpClient.GetAsync($"course/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return responseSuccess.Data; //response class'ındaki data
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
            var response = await _httpClient.PutAsJsonAsync<UpdateCourseInput>("course", updateCourseInput);

            return response.IsSuccessStatusCode;
        }
    }
}
