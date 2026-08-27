using AutoMapper;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Services
{
	public class StickerService : IStickerService
	{
		private readonly IGenericRepository<Sticker> _stickerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public StickerService(
			IGenericRepository<Sticker> stickerRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_stickerRepository = stickerRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<StickerDto>> GetAllAsync()
		{
			var stickers = await _stickerRepository.GetAllReadOnlyAsync();

			return _mapper.Map<IEnumerable<StickerDto>>(stickers);
		}

		public async Task<StickerDto?> GetByIdAsync(int id)
		{
			if (id <= 0)
				return null;

			var sticker = await _stickerRepository.GetByIdReadOnlyAsync(id);

			return sticker is null
				? null
				: _mapper.Map<StickerDto>(sticker);
		}

		public async Task<StickerDto> CreateAsync(StickerDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);

			var sticker = _mapper.Map<Sticker>(dto);

			await _stickerRepository.AddAsync(sticker);

			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<StickerDto>(sticker);
		}

		public async Task<bool> UpdateAsync(int id, StickerDto dto)
		{
			if (id <= 0)
				return false;

			ArgumentNullException.ThrowIfNull(dto);

			var sticker = await _stickerRepository.GetByIdAsync(id);

			if (sticker is null)
				return false;

			_mapper.Map(dto, sticker);

			_stickerRepository.Update(sticker);

			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			if (id <= 0)
				return false;

			var sticker = await _stickerRepository.GetByIdAsync(id);

			if (sticker is null)
				return false;

			_stickerRepository.Delete(sticker);

			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}