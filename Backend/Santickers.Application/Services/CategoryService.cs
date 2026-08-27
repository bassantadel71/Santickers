using AutoMapper;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IGenericRepository<Category> _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CategoryService(
			IGenericRepository<Category> categoryRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		

		public async Task<IEnumerable<CategoryDto>> GetAllAsync()
		{
			var categories = await _categoryRepository.GetAllReadOnlyAsync();

			return _mapper.Map<IEnumerable<CategoryDto>>(categories);
		}

		public async Task<CategoryDto?> GetByIdAsync(int id)
		{
			if (id <= 0)
				return null;

			var category = await _categoryRepository.GetByIdReadOnlyAsync(id);

			return category is null
				? null
				: _mapper.Map<CategoryDto>(category);
		}

		public async Task<CategoryDto> CreateAsync(CategoryDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);

			var category = _mapper.Map<Category>(dto);

			await _categoryRepository.AddAsync(category);

			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CategoryDto>(category);
		}

		public async Task<bool> UpdateAsync(int id, CategoryDto dto)
		{
			if (id <= 0)
				return false;

			ArgumentNullException.ThrowIfNull(dto);

			var category = await _categoryRepository.GetByIdAsync(id);

			if (category is null)
				return false;

			_mapper.Map(dto, category);

			_categoryRepository.Update(category);

			await _unitOfWork.SaveChangesAsync();

			return true;
		}


		public async Task<bool> DeleteAsync(int id)
		{
			if (id <= 0)
				return false;

			var category = await _categoryRepository.GetByIdAsync(id);

			if (category is null)
				return false;

			_categoryRepository.Delete(category);

			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}
