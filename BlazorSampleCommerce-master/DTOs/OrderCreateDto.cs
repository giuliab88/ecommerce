namespace BlazorSampleCommerce.DTOs
{
    public class OrderCreateDto
    {
        public Guid UserId { get; set; }
        public int AddressId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } = new();
    }

    public class OrderItemCreateDto
    {
        public Guid SkuId { get; set; }
        public int Quantity { get; set; }
    }
}
