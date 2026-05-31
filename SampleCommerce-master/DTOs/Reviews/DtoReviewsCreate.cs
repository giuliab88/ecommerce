namespace SampleCommerce.DTOs.Reviews
{
    public class DtoReviewsCreate
    {
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
