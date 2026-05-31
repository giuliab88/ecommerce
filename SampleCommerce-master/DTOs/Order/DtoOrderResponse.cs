namespace SampleCommerce.DTOs.Order
{
    public class DtoOrderResponse
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid UserId { get; set; }
        public int AddressId { get; set; }
    }
}
