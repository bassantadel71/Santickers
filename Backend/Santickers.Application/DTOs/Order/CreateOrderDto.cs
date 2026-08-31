using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs.Order
{
	public class CreateOrderDto
	{
		public string FullName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public string StreetAddress { get; set; } = string.Empty;

		public string City { get; set; } = string.Empty;

		public string Governorate { get; set; } = string.Empty;

		public string PostalCode { get; set; } = string.Empty;

		public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
	}
}
