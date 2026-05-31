using SampleCommerce.DTOs.OrderItem;

namespace SampleCommerce.DTOs.Order
{
    public class DtoOrderCreate
    {
        public Guid UserId { get; set; }
        public int AddressId { get; set; }
        public List<DtoOrderItemCreate> Items { get; set; } = new();
    }
}
