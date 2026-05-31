using SampleCommerce.DTOs.Address;

namespace SampleCommerce.DTOs.Address
{
    public class DtoAddressCreate : IAddressDto
    {
        public Guid UserId { get; set; }
        public string Country { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public string? Phone { get; set; }
        public string StreetAdress { get; set; } = null!;
        public string ComplementAddress { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public bool IsPreferred { get; set; }
    }
}