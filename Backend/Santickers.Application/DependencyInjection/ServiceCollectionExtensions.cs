using Microsoft.Extensions.DependencyInjection;
using Santickers.Application.Interfaces;
using Santickers.Application.Mapping;
using Santickers.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplication(
			this IServiceCollection services)
		{
			
			services.AddAutoMapper(cfg => { }, typeof(CategoryProfile).Assembly);
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IStickerService, StickerService>();
			services.AddScoped<IFavoriteService, FavoriteService>();
			services.AddScoped<IOrderService, OrderService>();

			return services;
		}
	}
}
