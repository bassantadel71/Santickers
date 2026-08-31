using Microsoft.EntityFrameworkCore;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Santickers.Infrastructure.Persistence.Data.Seeding
{
	/// <summary>
	/// Seeds sellable stickers from the physical sticker catalog folders inside
	/// <paramref name="stickersRootPath"/> (e.g. wwwroot/images/stickers).
	///
	/// Rules:
	///  - Every subfolder inside the root is a category (folder name = category name).
	///  - Every image file directly inside a category folder is a sellable Sticker,
	///    name = filename without extension, Price = 10, IsAvailable = true.
	///  - Image files at the root of the catalog are UI images only (not products).
	///  - Any existing sticker NOT sourced from a catalog category folder is treated
	///    as a legacy/old sticker and marked IsAvailable = false.
	///
	/// Idempotent: a sticker is identified by (Name + CategoryId). If it already
	/// exists, its values are updated instead of inserting a duplicate.
	/// </summary>
	public static class StickerSeeder
	{
		public static async Task SeedAsync(
			ApplicationDbContext context,
			string stickersRootPath)
		{
			var categories = await context.Categories
				.ToDictionaryAsync(c => c.Name, StringComparer.OrdinalIgnoreCase);

			// Remove legacy stickers and their categories that are no longer part
			// of the catalog.
			var removedCategoryNames = new[] { "Movies", "Gaming", "Boys", "Skate", "Flowers" };

			var removedStickerNames = new[]
			{
				"Bolt Shield", "Lofi Cat", "Sakura Wink", "Player One",
				"Stay Weird", "Movie Night", "Retro TV", "Flame Skater", "Daisy Flutter"
			};

			var stickersToRemove = await context.Stickers
				.Where(s =>
					removedStickerNames.Contains(s.Name)
					|| (s.Category != null && removedCategoryNames.Contains(s.Category.Name)))
				.ToListAsync();

			if (stickersToRemove.Count > 0)
			{
				context.Stickers.RemoveRange(stickersToRemove);
				await context.SaveChangesAsync();
			}

			// Now safe to remove the legacy categories (none of their stickers remain).
			var categoriesToRemove = await context.Categories
				.Where(c => removedCategoryNames.Contains(c.Name))
				.ToListAsync();

			if (categoriesToRemove.Count > 0)
			{
				context.Categories.RemoveRange(categoriesToRemove);
				await context.SaveChangesAsync();
			}

			var stickersByKey = await context.Stickers
				.ToDictionaryAsync(s => (s.Name, s.CategoryId));

			var knownCategoryIds = new HashSet<int>(categories.Values.Select(c => c.Id));

			var dir = new DirectoryInfo(stickersRootPath);

			if (dir.Exists)
			{
				foreach (var categoryFolder in dir.EnumerateDirectories())
				{
					var categoryName = categoryFolder.Name.Trim();

					if (!categories.TryGetValue(categoryName, out var category))
						continue;

					foreach (var image in categoryFolder.EnumerateFiles())
					{
						if (!IsImageFile(image))
							continue;

						var name = Path.GetFileNameWithoutExtension(image.Name).Trim();

						if (string.IsNullOrWhiteSpace(name))
							continue;

						var imageUrl = $"/{Path.Combine("images", "stickers", categoryFolder.Name, image.Name).Replace('\\', '/')}";

						var key = (name, category.Id);

						if (stickersByKey.TryGetValue(key, out var existing))
						{
							existing.Price = 10m;
							existing.IsAvailable = true;
							existing.ImageUrl = imageUrl;
							stickersByKey.Remove(key);
							continue;
						}

						var sticker = new Sticker
						{
							Name = name,
							CategoryId = category.Id,
							Price = 10m,
							ImageUrl = imageUrl,
							IsAvailable = true
						};

						context.Stickers.Add(sticker);
					}
				}
			}

			// Any remaining stickers that were not matched to a catalog image are
			// legacy/old stickers - keep them visible but not sellable.
			foreach (var old in stickersByKey.Values)
			{
				if (old.IsAvailable)
					old.IsAvailable = false;
			}

			await context.SaveChangesAsync();
		}

		private static bool IsImageFile(FileInfo file)
		{
			var ext = file.Extension.ToLowerInvariant();
			return ext is ".png" or ".jpg" or ".jpeg" or ".webp" or ".gif" or ".bmp";
		}
	}
}
