namespace SampleCommerce.DTOs.Users
{
    public interface IUserDto
    {
        string? Name { get; set; }
        string? Email { get; set; }
        bool Seller { get; set; }
        string? Iva { get; set; }
        string? TradingName { get; set; }
    }
}