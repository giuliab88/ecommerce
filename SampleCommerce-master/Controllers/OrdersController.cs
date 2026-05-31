using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Order;
using SampleCommerce.Models;
using SampleCommerce.Services;

namespace SampleCommerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : BaseController
    {
        private readonly OrdersService _service;
        public OrdersController(OrdersService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            Result<DtoOrderResponse> result = _service.Read(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);

        }
        [HttpGet("user")]
        public IActionResult GetByUser([FromQuery] Guid userId)
        {
            Result<List<DtoOrderResponse>> orders  = _service.ReadAllByUser(userId);
            return Ok(orders.Data);
        }

        [HttpPost]
        public IActionResult Create(DtoOrderCreate order)
        {
            Result<DtoOrderResponse> result = _service.Create(order);
            if (!result.Success)
                return BadRequest(result.Data);
            return CreatedAtAction(nameof(Read), new { id = result.Data!.Id }, result.Data);
        }
    }
}
