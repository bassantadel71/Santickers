using Microsoft.EntityFrameworkCore;
using Santickers.Infrastructure.Persistence.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Data
{
	public static class DbInitializer
	{
		public static async Task InitializeAsync(ApplicationDbContext context)
		{
			await context.Database.MigrateAsync();

			await CategorySeeder.SeedAsync(context);

			await StickerSeeder.SeedAsync(context);
		}
	}
}
