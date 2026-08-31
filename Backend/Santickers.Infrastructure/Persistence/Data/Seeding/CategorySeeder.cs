using Microsoft.EntityFrameworkCore;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Santickers.Infrastructure.Persistence.Data.Seeding
{
	public static class CategorySeeder
	{
		/// <summary>
		/// Seeds categories from the physical sticker catalog folders inside
		/// <paramref name="stickersRootPath"/> (e.g. wwwroot/images/stickers).
		/// Every subfolder becomes a category. Existing categories are matched
		/// case-insensitively so no duplicates are created on repeated runs.
		/// </summary>
		public static async Task EnsureCategoriesAsync(
			ApplicationDbContext context,
			string stickersRootPath)
		{
			var existing = await context.Categories
				.ToDictionaryAsync(c => c.Name, StringComparer.OrdinalIgnoreCase);

			var dir = new DirectoryInfo(stickersRootPath);

			if (dir.Exists)
			{
				foreach (var folder in dir.EnumerateDirectories())
				{
					var name = folder.Name.Trim();

					if (string.IsNullOrWhiteSpace(name))
						continue;

					if (existing.ContainsKey(name))
						continue;

					var category = new Category
					{
						Name = name,
						Description = $"{name} inspired stickers"
					};

					context.Categories.Add(category);
					existing.Add(name, category);
				}
			}

			await context.SaveChangesAsync();
		}
	}
}
