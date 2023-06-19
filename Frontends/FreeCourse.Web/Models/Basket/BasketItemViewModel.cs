namespace FreeCourse.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        private decimal? discountAppliedPrice { get; set; }

        public decimal CurrentPrice { get => discountAppliedPrice != null ? discountAppliedPrice.Value : Price; }
        public void AppliedDiscount(decimal discountPrice)
        {
            discountAppliedPrice = discountPrice;
        }
    }
}
