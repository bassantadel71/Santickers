using Microsoft.AspNetCore.Authorization;
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

		public PaymentsController(
			IOrderService orderService,
			IPaymobService paymobService)
		{
			_orderService = orderService;
			_paymobService = paymobService;
		}

		[HttpPost("paymob-callback")]
		[AllowAnonymous]
		public async Task<IActionResult> PaymobCallback(
			[FromQuery] string hmac,
			[FromBody] PaymobCallbackPayload payload)
		{
			Console.WriteLine("🔥 PAYMOB CALLBACK HIT!");

			if (payload?.obj is null)
			{
				Console.WriteLine("❌ Paymob payload is null.");
				return BadRequest();
			}

			var flatData = payload.obj.ToFlatDictionary();

			if (!_paymobService.VerifyHmac(flatData, hmac))
			{
				Console.WriteLine("❌ Invalid Paymob HMAC.");
				return Unauthorized();
			}

			var paymobOrderId =
				payload.obj.order?.id.ToString() ?? string.Empty;

			var transactionId =
				payload.obj.id.ToString();

			Console.WriteLine("========== PAYMOB RESULT ==========");
			Console.WriteLine($"Transaction ID: {payload.obj.id}");
			Console.WriteLine($"Paymob Order ID: {payload.obj.order?.id}");
			Console.WriteLine($"Amount: {payload.obj.amount_cents}");
			Console.WriteLine($"Currency: {payload.obj.currency}");
			Console.WriteLine($"Success: {payload.obj.success}");
			Console.WriteLine($"Pending: {payload.obj.pending}");
			Console.WriteLine($"Error Occurred: {payload.obj.error_occured}");
			Console.WriteLine($"Is Auth: {payload.obj.is_auth}");
			Console.WriteLine($"Is Capture: {payload.obj.is_capture}");
			Console.WriteLine($"Is Refunded: {payload.obj.is_refunded}");
			Console.WriteLine($"Is Voided: {payload.obj.is_voided}");
			Console.WriteLine("==================================");

			if (payload.obj.success)
			{
				Console.WriteLine(
					$"✅ Payment successful. Paymob Order: {paymobOrderId}");

				await _orderService.MarkPaidAsync(
					paymobOrderId,
					transactionId);
			}
			else
			{
				Console.WriteLine(
					$"❌ Payment failed. Paymob Order: {paymobOrderId}");

				await _orderService.MarkFailedAsync(
					paymobOrderId);
			}

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

		public int amount_cents { get; set; }

		public string? created_at { get; set; }

		public string? currency { get; set; }

		public bool error_occured { get; set; }

		public bool has_parent_transaction { get; set; }

		public int integration_id { get; set; }

		public bool is_3d_secure { get; set; }

		public bool is_auth { get; set; }

		public bool is_capture { get; set; }

		public bool is_refunded { get; set; }

		public bool is_standalone_payment { get; set; }

		public bool is_voided { get; set; }

		public PaymobOrderRef? order { get; set; }

		public int owner { get; set; }

		public bool pending { get; set; }

		public PaymobSourceData? source_data { get; set; }

		public bool success { get; set; }

		public IDictionary<string, string> ToFlatDictionary()
		{
			return new Dictionary<string, string>
			{
				["amount_cents"] = amount_cents.ToString(),
				["created_at"] = created_at ?? string.Empty,
				["currency"] = currency ?? string.Empty,
				["error_occured"] = error_occured
					.ToString()
					.ToLowerInvariant(),

				["has_parent_transaction"] = has_parent_transaction
					.ToString()
					.ToLowerInvariant(),

				["id"] = id.ToString(),

				["integration_id"] = integration_id.ToString(),

				["is_3d_secure"] = is_3d_secure
					.ToString()
					.ToLowerInvariant(),

				["is_auth"] = is_auth
					.ToString()
					.ToLowerInvariant(),

				["is_capture"] = is_capture
					.ToString()
					.ToLowerInvariant(),

				["is_refunded"] = is_refunded
					.ToString()
					.ToLowerInvariant(),

				["is_standalone_payment"] = is_standalone_payment
					.ToString()
					.ToLowerInvariant(),

				["is_voided"] = is_voided
					.ToString()
					.ToLowerInvariant(),

				["order"] = order?.id.ToString() ?? string.Empty,

				["owner"] = owner.ToString(),

				["pending"] = pending
					.ToString()
					.ToLowerInvariant(),

				["source_data_pan"] =
					source_data?.pan ?? string.Empty,

				["source_data_sub_type"] =
					source_data?.sub_type ?? string.Empty,

				["source_data_type"] =
					source_data?.type ?? string.Empty,

				["success"] = success
					.ToString()
					.ToLowerInvariant()
			};
		}
	}

	public class PaymobSourceData
	{
		public string? pan { get; set; }

		public string? sub_type { get; set; }

		public string? type { get; set; }
	}

	public class PaymobOrderRef
	{
		public long id { get; set; }
	}
}

