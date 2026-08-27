using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Santickers.Domain.Entities;
using Santickers.Infrastructure.Persistence.Data;


namespace Santickers.Infrastructure.Persistence.Data.Seeding
{
	public static class CategorySeeder
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			if (await context.Categories.AnyAsync())
				return;

			var categories = new List<Category>
			{
				new()
				{
					Name = "Anime",
					Description = "Anime and manga inspired stickers"
				},
				new()
				{
					Name = "Girls",
					Description = "Cute and aesthetic stickers for girls"
				},
				new()
				{
					Name = "Quotes",
					Description = "Fun, inspiring and relatable quote stickers"
				},
				new()
				{
					Name = "Marvel",
					Description = "Marvel heroes and characters stickers"
				},
				new()
				{
					Name = "Football",
					Description = "Football clubs, players and football themed stickers"
				},
				new()
				{
					Name = "Custom",
					Description = "Custom stickers designed just for you"
				}
			};

			await context.Categories.AddRangeAsync(categories);
			await context.SaveChangesAsync();
		}
	}
}
