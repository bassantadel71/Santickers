using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Santickers.Application.DTOs.Order;
using Santickers.Application.Interfaces;
using System.Security.Claims;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpPost]
		public async Task<ActionResult<CreateOrderResultDto>> Create(CreateOrderDto dto)
		{
			var userId = GetUserId();

			var result = await _orderService.CreateAsync(userId, dto);

			if (result is null)
				return BadRequest(new { error = "Your cart is empty or contains invalid items." });

			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<OrderDto>> GetById(int id)
		{
			var userId = GetUserId();

			var order = await _orderService.GetByIdAsync(id, userId);

			if (order is null)
				return NotFound();

			return Ok(order);
		}

		private Guid GetUserId()
		{
			var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
				?? User.FindFirstValue("sub");

			return Guid.Parse(sub!);
		}
	}
}
