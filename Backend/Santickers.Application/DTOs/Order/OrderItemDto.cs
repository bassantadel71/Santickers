using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs.Order
{
	public class OrderItemDto
	{
		public int Id { get; set; }

		public int StickerId { get; set; }

		public string StickerName { get; set; } = string.Empty;

		public decimal UnitPrice { get; set; }

		public int Quantity { get; set; }
	}
}
