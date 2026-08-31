using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Payments.Settings
{
	public class PaymobSettings
	{
		public string ApiKey { get; set; } = string.Empty;

		public string IntegrationId { get; set; } = string.Empty;

		public string IframeId { get; set; } = string.Empty;

		public string HmacSecret { get; set; } = string.Empty;

		public string BaseUrl { get; set; } = "https://accept.paymob.com/api";
	}
}
