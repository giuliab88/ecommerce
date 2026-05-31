using SampleCommerce.DTOs.SKUs;

namespace SampleCommerce.DTOs.Product
{
    public class DtoProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int TotalReviews { get; set; }
        public double MediaReviews { get; set; }
        public Guid SellerId { get; set; }
        public List<DtoSkusResponse> StockKeepingUnits { get; set; } = new();
    }
}
