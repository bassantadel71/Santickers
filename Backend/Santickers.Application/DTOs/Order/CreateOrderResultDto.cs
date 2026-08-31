using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs.Order
{
	public class CreateOrderResultDto
	{
		public OrderDto Order { get; set; } = null!;

		public string PaymentIframeUrl { get; set; } = string.Empty;
	}
}
