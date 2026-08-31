using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public class PaymobBillingData
	{
		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = "N/A";

		public string Email { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;
	}
	public interface IPaymobService
	{
		Task<(string PaymobOrderId, string PaymentIframeUrl)> CreatePaymentAsync(
			int localOrderId,
			decimal amount,
			PaymobBillingData billing);

		bool VerifyHmac(IDictionary<string, string> callbackData, string receivedHmac);
	}
}
