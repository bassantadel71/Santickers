using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StickersController : ControllerBase
	{
		private readonly IStickerService _stickerService;

		public StickersController(IStickerService stickerService)
		{
			_stickerService = stickerService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StickerDto>>> GetAll()
		{
			var stickers = await _stickerService.GetAllAsync();

			return Ok(stickers);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<StickerDto>> GetById(int id)
		{
			var sticker = await _stickerService.GetByIdAsync(id);

			if (sticker is null)
				return NotFound();

			return Ok(sticker);
		}

		[HttpPost]
		public async Task<ActionResult<StickerDto>> Create(StickerDto dto)
		{
			var sticker = await _stickerService.CreateAsync(dto);

			return CreatedAtAction(
				nameof(GetById),
				new { id = sticker.Id },
				sticker);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, StickerDto dto)
		{
			var updated = await _stickerService.UpdateAsync(id, dto);

			if (!updated)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _stickerService.DeleteAsync(id);

			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}