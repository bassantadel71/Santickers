using Santickers.Application.DTOs;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace Santickers.Application.Mapping
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CategoryDto>();

			CreateMap<CategoryDto, Category>();
		}
	}
}
