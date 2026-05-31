namespace SampleCommerce.DTOs.Auth
{
    public class DtoResetPasswordRequest
    {
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
