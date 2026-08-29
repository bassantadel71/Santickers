using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Santickers.Application.Interfaces
{
	public interface IFavoriteService
	{
		Task<IEnumerable<int>> GetUserFavoriteIdsAsync(Guid userId);

		Task<bool> AddFavoriteAsync(Guid userId, int stickerId);

		Task<bool> RemoveFavoriteAsync(Guid userId, int stickerId);
	}
}