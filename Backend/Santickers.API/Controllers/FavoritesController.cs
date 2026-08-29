using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Santickers.Application.Interfaces;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class FavoritesController : ControllerBase
	{
		private readonly IFavoriteService _favoriteService;

		public FavoritesController(IFavoriteService favoriteService)
		{
			_favoriteService = favoriteService;
		}

		private Guid CurrentUserId
		{
			get
			{
				var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
					?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

				if (!Guid.TryParse(idClaim, out var userId))
					throw new InvalidOperationException("Authenticated user id is missing or invalid.");

				return userId;
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<int>>> GetFavorites()
		{
			var ids = await _favoriteService.GetUserFavoriteIdsAsync(CurrentUserId);

			return Ok(ids);
		}

		[HttpPost("{stickerId:int}")]
		public async Task<IActionResult> AddFavorite(int stickerId)
		{
			var added = await _favoriteService.AddFavoriteAsync(CurrentUserId, stickerId);

			if (!added)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{stickerId:int}")]
		public async Task<IActionResult> RemoveFavorite(int stickerId)
		{
			var removed = await _favoriteService.RemoveFavoriteAsync(CurrentUserId, stickerId);

			if (!removed)
				return NotFound();

			return NoContent();
		}
	}
}