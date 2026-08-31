using Santickers.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public interface IOrderService
	{
		Task<CreateOrderResultDto?> CreateAsync(Guid userId, CreateOrderDto dto);

		Task<OrderDto?> GetByIdAsync(int id, Guid userId);

		Task MarkPaidAsync(string paymobOrderId, string paymobTransactionId);

		Task MarkFailedAsync(string paymobOrderId);
	}
}
