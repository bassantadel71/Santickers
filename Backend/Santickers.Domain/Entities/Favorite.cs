using System;

namespace Santickers.Domain.Entities
{
	public class Favorite
	{
		public Guid UserId { get; set; }

		public int StickerId { get; set; }

		public Sticker Sticker { get; set; } = null!;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}