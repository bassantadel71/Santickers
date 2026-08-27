using Santickers.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDto>> GetAllAsync();

		Task<CategoryDto?> GetByIdAsync(int id);

		Task<CategoryDto> CreateAsync(CategoryDto dto);

		Task<bool> UpdateAsync(int id, CategoryDto dto);

		Task<bool> DeleteAsync(int id);
	}
}
