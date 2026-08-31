using Microsoft.Extensions.Options;
using Santickers.Application.Interfaces;
using Santickers.Infrastructure.Payments.Settings;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Santickers.Infrastructure.Payments
{
	public class PaymobService : IPaymobService
	{
		private readonly HttpClient _httpClient;
		private readonly PaymobSettings _settings;

		public PaymobService(
			HttpClient httpClient,
			IOptions<PaymobSettings> settings)
		{
			_httpClient = httpClient;
			_settings = settings.Value;
		}

		public async Task<(string PaymobOrderId, string PaymentIframeUrl)> CreatePaymentAsync(
			int localOrderId,
			decimal amount,
			PaymobBillingData billing)
		{
			var amountCents = (int)(amount * 100);

			// 1. Get authentication token
			var authResponse = await _httpClient.PostAsJsonAsync(
				$"{_settings.BaseUrl}/auth/tokens",
				new
				{
					api_key = _settings.ApiKey
				});

			authResponse.EnsureSuccessStatusCode();

			var authJson =
				await authResponse.Content.ReadFromJsonAsync<JsonElement>();

			var authToken = authJson
				.GetProperty("token")
				.GetString();

			if (string.IsNullOrEmpty(authToken))
				throw new Exception("Failed to get Paymob authentication token.");

			// 2. Create Paymob order
			var orderResponse = await _httpClient.PostAsJsonAsync(
				$"{_settings.BaseUrl}/ecommerce/orders",
				new
				{
					auth_token = authToken,
					delivery_needed = "false",
					amount_cents = amountCents,
					currency = "EGP",
					merchant_order_id = localOrderId.ToString(),
					items = Array.Empty<object>()
				});

			orderResponse.EnsureSuccessStatusCode();

			var orderJson =
				await orderResponse.Content.ReadFromJsonAsync<JsonElement>();

			var paymobOrderId = orderJson
				.GetProperty("id")
				.GetInt64()
				.ToString();

			// 3. Get payment key
			var paymentKeyResponse = await _httpClient.PostAsJsonAsync(
				$"{_settings.BaseUrl}/acceptance/payment_keys",
				new
				{
					auth_token = authToken,
					amount_cents = amountCents,
					expiration = 3600,
					order_id = paymobOrderId,

					billing_data = new
					{
						first_name = billing.FirstName,
						last_name = billing.LastName,
						email = billing.Email,
						phone_number = billing.Phone,

						apartment = "NA",
						floor = "NA",
						street = "NA",
						building = "NA",
						city = "NA",
						country = "EG",
						state = "NA"
					},

					currency = "EGP",
					integration_id = _settings.IntegrationId
				});

			paymentKeyResponse.EnsureSuccessStatusCode();

			var paymentKeyJson =
				await paymentKeyResponse.Content.ReadFromJsonAsync<JsonElement>();

			var paymentToken = paymentKeyJson
				.GetProperty("token")
				.GetString();

			if (string.IsNullOrEmpty(paymentToken))
				throw new Exception("Failed to get Paymob payment token.");

			// 4. Generate iframe URL
			var iframeUrl =
				$"https://accept.paymob.com/api/acceptance/iframes/" +
				$"{_settings.IframeId}?payment_token={paymentToken}";

			return (paymobOrderId, iframeUrl);
		}

		public bool VerifyHmac(
			IDictionary<string, string> callbackData,
			string receivedHmac)
		{
			if (string.IsNullOrWhiteSpace(receivedHmac))
				return false;

			var orderedKeys = new[]
			{
				"amount_cents",
				"created_at",
				"currency",
				"error_occured",
				"has_parent_transaction",
				"id",
				"integration_id",
				"is_3d_secure",
				"is_auth",
				"is_capture",
				"is_refunded",
				"is_standalone_payment",
				"is_voided",
				"order",
				"owner",
				"pending",
				"source_data_pan",
				"source_data_sub_type",
				"source_data_type",
				"success"
			};

			var concatenated = string.Concat(
				orderedKeys.Select(key =>
					callbackData.TryGetValue(key, out var value)
						? value
						: string.Empty
				)
			);

			var keyBytes =
				Encoding.UTF8.GetBytes(_settings.HmacSecret);

			var messageBytes =
				Encoding.UTF8.GetBytes(concatenated);

			using var hmac = new HMACSHA512(keyBytes);

			var hash = hmac.ComputeHash(messageBytes);

			var computedHmac =
				Convert.ToHexString(hash)
					.ToLowerInvariant();

			return string.Equals(
				computedHmac,
				receivedHmac,
				StringComparison.OrdinalIgnoreCase);
		}
	}
}