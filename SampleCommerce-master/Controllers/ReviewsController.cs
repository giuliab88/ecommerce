using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Reviews;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : BaseController
    {
        private readonly ReviewService _service;
        public ReviewsController(ReviewService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] DtoReviewsCreate dto)
        {
            Result<DtoReviewsResponse> result = _service.Create(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(Read), new { id = result.Data!.Id }, result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            Result<DtoReviewsResponse> result = _service.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            Result<List<DtoReviewsResponse>>? result = _service.ReadAll();
            return Ok(result.Data);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DtoReviewsUpdate userDto)
        {
            Result<DtoReviewsResponse> result = _service.Update(id, userDto);
            return ProcessResult(result, () => Ok(result.Data));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Result result = _service.Delete(id);
            return ProcessResult(result, () => NoContent());
        }
    }
}
