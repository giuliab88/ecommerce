using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Users;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly UserService _service;
        private readonly IEmailService _emailService;

        public UserController(UserService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoUserCreateRequest userDto)
        {
            Result<DtoUserResponse> result = _service.Create(userDto);
            if (!result.Success)
                return BadRequest(result);

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            await _service.SendConfirmationEmailAsync(result.Data!.Id, result.Data!.Email, result.Data!.Name, _emailService, baseUrl);

            return CreatedAtAction(nameof(Read), new { id = result.Data!.Id }, result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Read(Guid id)
        {
            Result<DtoUserResponse> result = _service.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            Result<List<DtoUserResponse>>? result = _service.ReadAll();
            return Ok(result.Data);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] DtoUserUpdateRequest userDto)
        {
            Result<DtoUserResponse> result = _service.Update(id, userDto);
            return ProcessResult(result, () => Ok(result.Data));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Result result = _service.Delete(id);
            return ProcessResult(result, () => NoContent());
        }
    }
}
