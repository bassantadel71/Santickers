using Santickers.Application.DTOs;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Santickers.Application.Mapping
{
	public class StickerProfile : Profile
	{
		public StickerProfile()
		{
			CreateMap<Sticker, StickerDto>();

			CreateMap<StickerDto, Sticker>();
		}
	}
}