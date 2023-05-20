namespace FreeCourse.Web.Models.Catalog
{
    public class CreateCourseInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public FeatureViewModel FeatureViewModel { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
