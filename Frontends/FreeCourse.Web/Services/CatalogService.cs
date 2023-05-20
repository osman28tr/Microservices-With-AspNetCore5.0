using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
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

        public Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CourseViewModel> GetCourseById(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
