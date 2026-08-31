using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Domain.Entities
{
	public class OrderItem : BaseEntity
	{
		public int OrderId { get; set; }

		public Order Order { get; set; } = null!;

		public int StickerId { get; set; }

		public Sticker Sticker { get; set; } = null!;

		public string StickerName { get; set; } = string.Empty;

		public decimal UnitPrice { get; set; }

		public int Quantity { get; set; }
	}
}
