namespace SampleCommerce.DTOs.Users
{
    public class DtoUserResponse : IUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Seller { get; set; }
        public string? Iva { get; set; }
        public string? TradingName { get; set; }
    }
}