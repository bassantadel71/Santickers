using AutoMapper;
using Santickers.Application.DTOs.Order;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Mapping
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

			CreateMap<OrderItem, OrderItemDto>();
		}
	}
}
