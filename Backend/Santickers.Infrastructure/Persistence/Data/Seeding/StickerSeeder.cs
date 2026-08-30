using Microsoft.EntityFrameworkCore;
using Santickers.Domain.Entities;
using Santickers.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Data.Seeding
{
	public static class StickerSeeder
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			if (await context.Stickers.AnyAsync())
				return;

			var categories = await context.Categories.ToDictionaryAsync(c => c.Name);

			var stickers = new List<Sticker>
			{
				new()
				{
					Name = "Lofi Cat",
					CategoryId = categories["Girls"].Id,
					Price = 25,
					ImageUrl = "/images/stickers/cat.png",
					IsAvailable = true,
					Description = "A cozy little cat with headphones on, made for late-night study sessions. Printed on matte waterproof vinyl that stays put through every coffee spill."
				},
				new()
				{
					Name = "Sakura Wink",
					CategoryId = categories["Anime"].Id,
					Price = 30,
					ImageUrl = "/images/stickers/anime-girl.png",
					IsAvailable = true,
					Description = "Soft pink anime portrait with a playful wink. Die-cut edges, ultra-thin lamination, and colors that don't fade in sunlight."
				},
				new()
				{
					Name = "Player One",
					CategoryId = categories["Gaming"].Id,
					Price = 28,
					ImageUrl = "/images/stickers/gaming.png",
					IsAvailable = true,
					Description = "A retro controller with a heart in the middle for everyone who grew up on split-screen. Scratch resistant and bubble-free to apply."
				},
				new()
				{
					Name = "Stay Weird",
					CategoryId = categories["Quotes"].Id,
					Price = 22,
					ImageUrl = "/images/stickers/quote.png",
					IsAvailable = true,
					Description = "Hand-lettered script in our signature peach. A tiny daily reminder that being different is the whole point."
				},
				new()
				{
					Name = "Bolt Shield",
					CategoryId = categories["Marvel"].Id,
					Price = 32,
					ImageUrl = "/images/stickers/marvel.png",
					IsAvailable = true,
					Description = "A bold comic-book emblem with heavy outlines that read beautifully from across the room. Perfect for a laptop lid centerpiece."
				},
				new()
				{
					Name = "Movie Night",
					CategoryId = categories["Movies"].Id,
					Price = 27,
					ImageUrl = "/images/stickers/movies.png",
					IsAvailable = true,
					Description = "Clapperboard and popcorn for the person who always picks the film. Warm cream tones that pair with any setup."
				},
				new()
				{
					Name = "Retro TV",
					CategoryId = categories["Series"].Id,
					Price = 26,
					ImageUrl = "/images/stickers/series.png",
					IsAvailable = true,
					Description = "One more episode. A vintage television in peach and navy, die-cut down to the antennas."
				},
				new()
				{
					Name = "Flame Skater",
					CategoryId = categories["Boys"].Id,
					Price = 29,
					ImageUrl = "/images/stickers/skate.png",
					IsAvailable = true,
					Description = "Sunglasses, flames and a whole lot of attitude. Thick vinyl that survives backpacks, bottles and skate decks."
				},
				new()
				{
					Name = "Daisy Flutter",
					CategoryId = categories["Girls"].Id,
					Price = 24,
					ImageUrl = "/images/stickers/flowers.png",
					IsAvailable = true,
					Description = "Delicate daisies with a navy-edged butterfly. Soft, romantic and surprisingly durable."
				},
				new()
				{
					Name = "Launch Day",
					CategoryId = categories["Boys"].Id,
					Price = 26,
					ImageUrl = "/images/stickers/rocket.png",
					IsAvailable = true,
					Description = "A little rocket for big plans. Cream body, peach flame, and stars that catch the light."
				}
			};

			await context.Stickers.AddRangeAsync(stickers);
			await context.SaveChangesAsync();
		}
	}
}