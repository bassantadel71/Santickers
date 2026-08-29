using Microsoft.EntityFrameworkCore;
using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using Santickers.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Santickers.Infrastructure.Repositories
{
	public class FavoriteRepository : IFavoriteRepository
	{
		private readonly ApplicationDbContext _context;

		public FavoriteRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<int>> GetFavoriteStickerIdsAsync(Guid userId)
		{
			return await _context.Favorites
				.AsNoTracking()
				.Where(f => f.UserId == userId)
				.Select(f => f.StickerId)
				.ToListAsync();
		}

		public async Task<Favorite?> GetAsync(Guid userId, int stickerId)
		{
			return await _context.Favorites
				.FindAsync(userId, stickerId);
		}

		public async Task AddAsync(Favorite favorite)
		{
			await _context.Favorites.AddAsync(favorite);
		}

		public void Remove(Favorite favorite)
		{
			_context.Favorites.Remove(favorite);
		}
	}
}