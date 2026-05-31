namespace SampleCommerce.DTOs.Address
{
    public class DtoAddressUpdate : IAddressDto
    {
        public Guid UserId { get; set; }
        public string? Country { get; set; }
        public string? Receiver { get; set; }
        public string? Phone { get; set; }
        public string? StreetAdress { get; set; }
        public string? ComplementAddress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public bool IsPreferred { get; set; }
    }
}