namespace SampleCommerce.DTOs.SKUs
{
    public class DtoSkusCreate
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Dictionary<string, string> Features { get; set; } = new();
    }
}
