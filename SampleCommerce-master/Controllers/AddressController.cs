using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Address;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController
    {
        private readonly AddressService _service;
        public AddressController(AddressService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DtoAddressCreate dto)
        {
            Result<DtoAddressResponse> result = _service.Create(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(Read), new { id = result.Data!.Id }, result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            Result<DtoAddressResponse> result = _service.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            Result<List<DtoAddressResponse>>? result = _service.ReadAll();
            return Ok(result.Data);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(Guid userId)
        {
            Result<List<DtoAddressResponse>>? result = _service.ReadAllByUser(userId);
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DtoAddressUpdate userDto)
        {
            Result<DtoAddressResponse> result = _service.Update(id, userDto);
            return ProcessResult(result, () => Ok(result.Data));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] Guid userId)
        {
            Result result = _service.Delete(id, userId);
            return ProcessResult(result, () => NoContent());
        }
    }
}
