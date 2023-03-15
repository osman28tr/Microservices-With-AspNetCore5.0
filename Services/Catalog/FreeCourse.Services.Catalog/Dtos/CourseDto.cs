﻿using FreeCourse.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CourseDto
    {       
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public CategoryDto Category { get; set; }
    }
}