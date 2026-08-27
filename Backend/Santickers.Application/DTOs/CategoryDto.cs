using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs
{
	public class CategoryDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }
	}
}
