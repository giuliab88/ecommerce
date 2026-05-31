namespace SampleCommerce.DTOs.SKUs
{
    public class DtoSkuEmbeddedCreate
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Dictionary<string, string> Features { get; set; } = new();
    }
}
