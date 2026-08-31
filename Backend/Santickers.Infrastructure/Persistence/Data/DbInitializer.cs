using Microsoft.EntityFrameworkCore;
using Santickers.Infrastructure.Persistence.Data.Seeding;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Santickers.Infrastructure.Persistence.Data
{
	public static class DbInitializer
	{
		/// <summary>
		/// Initializes the database and seeds data. <paramref name="stickersRootPath"/>
		/// points to the physical sticker catalog directory (e.g. wwwroot/images/stickers).
		/// </summary>
		public static async Task InitializeAsync(
			ApplicationDbContext context,
			string stickersRootPath)
		{
			await context.Database.MigrateAsync();

			await CategorySeeder.EnsureCategoriesAsync(context, stickersRootPath);

			await StickerSeeder.SeedAsync(context, stickersRootPath);
		}
	}
}
