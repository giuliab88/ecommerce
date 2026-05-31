using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Product;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Result<List<DtoProductResponse>>? result =_productService.ReadAll();
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Result<DtoProductResponse>? result = _productService.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] DtoProductCreate dto)
        {
            Result<DtoProductResponse> result = _productService.Create(dto);
            if (!result.Success)
                return BadRequest(result);
            return CreatedAtAction(nameof(Get), new { id = result.Data!.Id }, result.Data);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] DtoProductUpdate dto)
        {
            Result<DtoProductResponse> result = _productService.Update(id, dto);
            return ProcessResult(result, () => Ok(result.Data));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Result result = _productService.Delete(id);
            return ProcessResult(result, () => NoContent());
        }
    }
}
