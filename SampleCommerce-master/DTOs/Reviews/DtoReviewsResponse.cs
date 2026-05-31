namespace SampleCommerce.DTOs.Reviews
{
    public class DtoReviewsResponse
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
