using AutoMapper;
using Santickers.Application.DTOs.Order;
using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Services
{
	public class OrderService : IOrderService
	{
		private const decimal FlatShippingFee = 30m;


		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IGenericRepository<Sticker> _stickerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymobService _paymobService;
		private readonly IMapper _mapper;


		public OrderService(
			IGenericRepository<Order> orderRepository,
			IGenericRepository<Sticker> stickerRepository,
			IUnitOfWork unitOfWork,
			IPaymobService paymobService,
			IMapper mapper)
		{
			_orderRepository = orderRepository;
			_stickerRepository = stickerRepository;
			_unitOfWork = unitOfWork;
			_paymobService = paymobService;
			_mapper = mapper;
		}


		public async Task<CreateOrderResultDto?> CreateAsync(Guid userId, CreateOrderDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);

			if (dto.Items is null || dto.Items.Count == 0)
				return null;

			var order = new Order
			{
				UserId = userId,
				FullName = dto.FullName,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				StreetAddress = dto.StreetAddress,
				City = dto.City,
				Governorate = dto.Governorate,
				PostalCode = dto.PostalCode,
				ShippingFee = FlatShippingFee,
				Status = OrderStatus.Pending
			};

			decimal subtotal = 0;

			foreach (var itemDto in dto.Items)
			{
				var sticker = await _stickerRepository.GetByIdAsync(itemDto.StickerId);

				if (sticker is null || itemDto.Quantity <= 0)
					continue;

				var lineTotal = sticker.Price * itemDto.Quantity;
				subtotal += lineTotal;

				order.Items.Add(new OrderItem
				{
					StickerId = sticker.Id,
					StickerName = sticker.Name,
					UnitPrice = sticker.Price,
					Quantity = itemDto.Quantity
				});
			}

			if (order.Items.Count == 0)
				return null;

			order.Subtotal = subtotal;
			order.Total = subtotal + order.ShippingFee;

			await _orderRepository.AddAsync(order);
			await _unitOfWork.SaveChangesAsync();

			var nameParts = dto.FullName.Split(' ', 2);

			var (paymobOrderId, iframeUrl) = await _paymobService.CreatePaymentAsync(
				order.Id,
				order.Total,
				new PaymobBillingData
				{
					FirstName = nameParts.Length > 0 ? nameParts[0] : dto.FullName,
					LastName = nameParts.Length > 1 ? nameParts[1] : "N/A",
					Email = dto.Email,
					Phone = dto.PhoneNumber
				});

			order.PaymobOrderId = paymobOrderId;
			_orderRepository.Update(order);
			await _unitOfWork.SaveChangesAsync();

			return new CreateOrderResultDto
			{
				Order = _mapper.Map<OrderDto>(order),
				PaymentIframeUrl = iframeUrl
			};
		}

		public async Task<OrderDto?> GetByIdAsync(int id, Guid userId)
		{
			var order = await _orderRepository.GetByIdReadOnlyAsync(id, x => x.Items);

			if (order is null || order.UserId != userId)
				return null;

			return _mapper.Map<OrderDto>(order);
		}

		public async Task MarkFailedAsync(string paymobOrderId)
		{
			var order = await FindByPaymobOrderIdAsync(paymobOrderId);

			if (order is null)
				return;

			order.Status = OrderStatus.Failed;

			_orderRepository.Update(order);
			await _unitOfWork.SaveChangesAsync();
		}
		private async Task<Order?> FindByPaymobOrderIdAsync(string paymobOrderId)
		{
			var orders = await _orderRepository.GetAllAsync();
			return orders.FirstOrDefault(o => o.PaymobOrderId == paymobOrderId);
		}

		public async Task MarkPaidAsync(string paymobOrderId, string paymobTransactionId)
		{
			var order = await FindByPaymobOrderIdAsync(paymobOrderId);

			if (order is null)
				return;

			order.Status = OrderStatus.Failed;

			_orderRepository.Update(order);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
