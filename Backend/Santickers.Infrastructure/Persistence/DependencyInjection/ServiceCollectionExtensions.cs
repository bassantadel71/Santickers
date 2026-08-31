using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Santickers.Application.Interfaces;
using Santickers.Infrastructure.Identity;
using Santickers.Infrastructure.Identity.Settings;
using Santickers.Infrastructure.Payments;
using Santickers.Infrastructure.Payments.Settings;
using Santickers.Infrastructure.Persistence.Data;
using Santickers.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection")));

		services.AddIdentityCore<ApplicationUser>(options =>
		{
			options.Password.RequireNonAlphanumeric = true;
			options.Password.RequiredLength = 8;
			options.Password.RequireUppercase = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireDigit = true;
		})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

		services.Configure<JwtSettings>(
			configuration.GetSection("Jwt"));

		services.AddScoped<IAuthService, AuthService>();

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddScoped<IFavoriteRepository, FavoriteRepository>();

		services.AddScoped(
			typeof(IGenericRepository<>),
			typeof(GenericRepository<>));

		services.Configure<PaymobSettings>(configuration.GetSection("Paymob"));
		services.AddHttpClient<IPaymobService, PaymobService>();



		return services;
	}
}