using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollections;
        private readonly IMongoCollection<Category> _categoryCollections;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndPoint;
        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings,IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollections = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollections = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndPoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollections.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollections.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollections.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course != null)
            {
                course.Category = await _categoryCollections.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
            }
            else
            {
                return Shared.Dtos.Response<CourseDto>.Fail("Course not found", 404);
            }
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserId(string id)
        {
            var courses = await _courseCollections.Find<Course>(x => x.UserId == id).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollections.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreatedDate = DateTime.Now;
            await _courseCollections.InsertOneAsync(newCourse);

            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = _courseCollections.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result == null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            }

            await _publishEndPoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent
            {
                CourseId = updateCourse.Id,
                UpdatedName = updateCourse.Name,
            });

            return Shared.Dtos.Response<NoContent>.Success(204);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollections.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Shared.Dtos.Response<NoContent>.Success(204);
            }
            else
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            }
        }
    }
}
