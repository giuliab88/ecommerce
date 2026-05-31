namespace SampleCommerce.DTOs.Users
{
    public class DtoUserUpdateRequest : IUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool Seller { get; set; }
        public string? Iva { get; set; }
        public string? TradingName { get; set; }
    }
}
