using SampleCommerce.DTOs.Users;

namespace SampleCommerce.DTOs.Auth
{
    public class DtoAuthLoginResponse
    {
        public string Token { get; set; } = null!;
        public DtoUserResponse User { get; set; } = null!;
    }
}
