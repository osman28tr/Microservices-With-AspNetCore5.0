using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.Catalog
{
    public class UpdateCourseInput
    {
        public string Id { get; set; }
		[Display(Name = "Kurs ismi")]
		public string Name { get; set; }
		[Display(Name = "Kurs açıklama")]
		public string Description { get; set; }
		[Display(Name = "Kurs fiyat")]
		public decimal Price { get; set; }
        public FeatureViewModel Feature { get; set; }
		[Display(Name = "Kategori")]
		public string CategoryId { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
		[Display(Name = "Kurs resmi")]
		public IFormFile PhotoFormFile { get; set; }
	}
}
