using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Domain.Entities
{
	public class Order : BaseEntity
	{
		public Guid UserId { get; set; }

		public string FullName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public string StreetAddress { get; set; } = string.Empty;

		public string City { get; set; } = string.Empty;

		public string Governorate { get; set; } = string.Empty;

		public string PostalCode { get; set; } = string.Empty;

		public decimal Subtotal { get; set; }

		public decimal ShippingFee { get; set; }

		public decimal Total { get; set; }

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public string PaymentMethod { get; set; } = "paymob";

		public string? PaymobOrderId { get; set; }

		public string? PaymobTransactionId { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
	}
}
