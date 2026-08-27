using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
		{
			var categories = await _categoryService.GetAllAsync();

			return Ok(categories);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<CategoryDto>> GetById(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);

			if (category is null)
				return NotFound();

			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult<CategoryDto>> Create(CategoryDto dto)
		{
			var category = await _categoryService.CreateAsync(dto);

			return CreatedAtAction(
				nameof(GetById),
				new { id = category.Id },
				category);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, CategoryDto dto)
		{
			var updated = await _categoryService.UpdateAsync(id, dto);

			if (!updated)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _categoryService.DeleteAsync(id);

			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}
