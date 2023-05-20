using FreeCourse.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetCourseById(string courseId);
        Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput);
        Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput);
        Task<bool> DeleteCourseAsync(string courseId);      
    }
}
