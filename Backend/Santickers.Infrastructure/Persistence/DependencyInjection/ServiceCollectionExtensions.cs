using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Santickers.Application.Interfaces;
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

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddScoped(
			typeof(IGenericRepository<>),
			typeof(GenericRepository<>));

		return services;
	}
}