using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Santickers.Application.Interfaces
{
	public interface IFavoriteRepository
	{
		Task<List<int>> GetFavoriteStickerIdsAsync(Guid userId);

		Task<Favorite?> GetAsync(Guid userId, int stickerId);

		Task AddAsync(Favorite favorite);

		void Remove(Favorite favorite);
	}
}