using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Santickers.Application.Interfaces;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IPaymobService _paymobService;

		public PaymentsController(IOrderService orderService, IPaymobService paymobService)
		{
			_orderService = orderService;
			_paymobService = paymobService;
		}

		[HttpPost("paymob-callback")]
		public async Task<IActionResult> PaymobCallback([FromQuery] string hmac, [FromBody] PaymobCallbackPayload payload)
		{
			if (payload?.obj is null)
				return BadRequest();

			var flatData = payload.obj.ToFlatDictionary();

			if (!_paymobService.VerifyHmac(flatData, hmac))
				return Unauthorized();

			var paymobOrderId = payload.obj.order?.id.ToString() ?? string.Empty;
			var transactionId = payload.obj.id.ToString();

			if (payload.obj.success)
				await _orderService.MarkPaidAsync(paymobOrderId, transactionId);
			else
				await _orderService.MarkFailedAsync(paymobOrderId);

			return Ok();
		}
	}

	public class PaymobCallbackPayload
	{
		public PaymobTransactionObj? obj { get; set; }
	}

	public class PaymobTransactionObj
	{
		public long id { get; set; }
		public bool success { get; set; }
		public PaymobOrderRef? order { get; set; }

		public IDictionary<string, string> ToFlatDictionary()
		{
			return new Dictionary<string, string>
			{
				["id"] = id.ToString(),
				["success"] = success.ToString().ToLowerInvariant(),
				["order"] = order?.id.ToString() ?? string.Empty
			};
		}
	

	public class PaymobOrderRef
	{
		public long id { get; set; }
	}
}
}
