using Santickers.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public interface IStickerService
	{
		Task<IEnumerable<StickerDto>> GetAllAsync();

		Task<StickerDto?> GetByIdAsync(int id);

		Task<StickerDto> CreateAsync(StickerDto dto);

		Task<bool> UpdateAsync(int id, StickerDto dto);

		Task<bool> DeleteAsync(int id);
	}
}