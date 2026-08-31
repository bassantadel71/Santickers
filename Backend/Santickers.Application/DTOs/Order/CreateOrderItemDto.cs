using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs.Order
{
	public class CreateOrderItemDto
	{
		public int StickerId { get; set; }

		public int Quantity { get; set; }
	}
}
