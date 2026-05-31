using Microsoft.AspNetCore.Mvc;
using SampleCommerce.DTOs.Auth;
using SampleCommerce.DTOs.Users;
using SampleCommerce.Common;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthController(UserService userService, JwtService jwtService, IEmailService emailService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DtoAuthLoginRequest dto)
        {
            Result<DtoUserResponse> result = _userService.Login(dto.Email, dto.Password);
            if (!result.Success)
                return Unauthorized(new { message = "Email o password non validi." });

            string token = _jwtService.GenerateToken(result.Data!);
            return Ok(new DtoAuthLoginResponse { Token = token, User = result.Data! });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] DtoForgotPasswordRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { message = "Email obbligatoria." });

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            await _userService.ForgotPasswordAsync(dto.Email, _emailService, baseUrl);
            return Ok(new { message = "Se l'indirizzo email è registrato, riceverai un link di ripristino." });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] DtoResetPasswordRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token) || string.IsNullOrWhiteSpace(dto.NewPassword))
                return BadRequest(new { message = "Token e nuova password sono obbligatori." });

            Result result = _userService.ResetPassword(dto.Token, dto.NewPassword);
            if (!result.Success)
                return BadRequest(new { message = result.Errors.FirstOrDefault() });

            return Ok(new { message = "Password aggiornata con successo." });
        }

        [HttpGet("confirm-email")]
        public IActionResult ConfirmEmail([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(new { message = "Token mancante." });

            Result result = _userService.ConfirmEmail(token);
            if (!result.Success)
                return BadRequest(new { message = result.Errors.FirstOrDefault() });

            return Ok(new { message = "Email confermata con successo." });
        }
    }
}
