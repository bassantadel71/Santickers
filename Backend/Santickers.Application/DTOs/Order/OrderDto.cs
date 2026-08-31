using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs.Order
{
	public class OrderDto
	{
		public int Id { get; set; }

		public string FullName { get; set; } = string.Empty;

		public decimal Subtotal { get; set; }

		public decimal ShippingFee { get; set; }

		public decimal Total { get; set; }

		public string Status { get; set; } = string.Empty;

		public string PaymentMethod { get; set; } = "paymob";

		public DateTime CreatedAt { get; set; }

		public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
	}
}
