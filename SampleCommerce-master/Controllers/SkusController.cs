using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.SKUs;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkusController : BaseController
    {
        private readonly SkuService _service;
        public SkusController(SkuService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DtoSkusCreate dto)
        {
            Result<DtoSkusResponse> result = _service.Create(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(Read), new { id = result.Data!.Id }, result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Read(Guid id)
        {
            Result<DtoSkusResponse> result = _service.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            Result<List<DtoSkusResponse>>? result = _service.ReadAll();
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] DtoSkusUpdate dto)
        {
            Result<DtoSkusResponse> result = _service.Update(id, dto);
            return ProcessResult(result, () => Ok(result.Data));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Result result = _service.Delete(id);
            return ProcessResult(result, () => NoContent());
        }
    }
}
