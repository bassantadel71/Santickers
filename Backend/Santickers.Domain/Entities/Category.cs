using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Domain.Entities
{
	public class Category : BaseEntity
	{

		public string Name { get; set; } = string.Empty;

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		public bool IsActive { get; set; } = true;

		public ICollection<Sticker> Stickers { get; set; } = new List<Sticker>();
	}
}
