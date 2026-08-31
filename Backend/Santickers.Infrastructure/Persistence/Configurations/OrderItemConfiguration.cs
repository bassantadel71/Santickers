using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.StickerName).IsRequired().HasMaxLength(150);
			builder.Property(i => i.UnitPrice).HasPrecision(18, 2);

			builder.HasOne(i => i.Sticker)
				.WithMany()
				.HasForeignKey(i => i.StickerId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
