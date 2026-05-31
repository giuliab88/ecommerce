using SampleCommerce.DTOs.SKUs;

namespace SampleCommerce.DTOs.Product
{
    public class DtoProductCreate
    {
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public Guid SellerId { get; set; }
        public List<DtoSkuEmbeddedCreate> StockKeepingUnits { get; set; } = new();
    }
}
