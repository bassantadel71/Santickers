using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Santickers.Application.Services
{
	public class FavoriteService : IFavoriteService
	{
		private readonly IFavoriteRepository _favoriteRepository;
		private readonly IGenericRepository<Sticker> _stickerRepository;
		private readonly IUnitOfWork _unitOfWork;

		public FavoriteService(
			IFavoriteRepository favoriteRepository,
			IGenericRepository<Sticker> stickerRepository,
			IUnitOfWork unitOfWork)
		{
			_favoriteRepository = favoriteRepository;
			_stickerRepository = stickerRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<int>> GetUserFavoriteIdsAsync(Guid userId)
		{
			return await _favoriteRepository.GetFavoriteStickerIdsAsync(userId);
		}

		public async Task<bool> AddFavoriteAsync(Guid userId, int stickerId)
		{
			if (stickerId <= 0)
				return false;

			var sticker = await _stickerRepository.GetByIdReadOnlyAsync(stickerId);

			if (sticker is null)
				return false;

			var existing = await _favoriteRepository.GetAsync(userId, stickerId);

			if (existing is null)
			{
				await _favoriteRepository.AddAsync(new Favorite
				{
					UserId = userId,
					StickerId = stickerId
				});

				await _unitOfWork.SaveChangesAsync();
			}

			return true;
		}

		public async Task<bool> RemoveFavoriteAsync(Guid userId, int stickerId)
		{
			if (stickerId <= 0)
				return false;

			var existing = await _favoriteRepository.GetAsync(userId, stickerId);

			if (existing is null)
				return false;

			_favoriteRepository.Remove(existing);

			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}