using Santickers.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Data.Seeding
{
	public static class StickerSeeder
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			// Sticker seed data will be added later.
			await Task.CompletedTask;
		}
	}
}
