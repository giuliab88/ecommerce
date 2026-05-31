namespace SampleCommerce.DTOs.Address
{
    public interface IAddressDto
    {
        string? Country { get; set; }
        string? Receiver { get; set; }
        string? Phone { get; set; }
        string? StreetAdress { get; set; }
        string? ComplementAddress { get; set; }
        string? ZipCode { get; set; }
        string? City { get; set; }
        string? Province { get; set; }
        bool IsPreferred { get; set; }
    }
}