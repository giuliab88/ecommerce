namespace SampleCommerce.DTOs.Users
{
    public class DtoUserCreateRequest : IUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Seller { get; set; }
        public string? Iva { get; set; }
        public string? TradingName { get; set; }
    }
}
