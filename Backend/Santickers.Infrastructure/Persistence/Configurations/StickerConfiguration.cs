using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Configurations
{
	public class StickerConfiguration : IEntityTypeConfiguration<Sticker>
	{
		public void Configure(EntityTypeBuilder<Sticker> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(s => s.Description)
				.HasMaxLength(1000);

			builder.Property(s => s.Price)
				.HasPrecision(18, 2);

			builder.Property(s => s.ImageUrl)
				.HasMaxLength(500);
		}
	}
}
