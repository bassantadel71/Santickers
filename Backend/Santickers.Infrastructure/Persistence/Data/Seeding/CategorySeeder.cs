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
					Name = "Boys",
					Description = "Cool and fun stickers for boys"
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
					Name = "Series",
					Description = "Shows, series and binge-worthy stickers"
				},
				new()
				{
					Name = "Movies",
					Description = "Movie and cinema themed stickers"
				},
				new()
				{
					Name = "Gaming",
					Description = "Games, controllers and gamer stickers"
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

			var existingNames = await context.Categories
				.Select(c => c.Name)
				.ToListAsync();

			var missing = categories
				.Where(c => !existingNames.Contains(c.Name))
				.ToList();

			if (missing.Count == 0)
				return;

			await context.Categories.AddRangeAsync(missing);
			await context.SaveChangesAsync();
		}
	}
}
