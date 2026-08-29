using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Santickers.Domain.Entities;
using System;

namespace Santickers.Infrastructure.Configurations
{
	public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
	{
		public void Configure(EntityTypeBuilder<Favorite> builder)
		{
			builder.HasKey(f => new { f.UserId, f.StickerId });

			builder.Property(f => f.CreatedAt)
				.IsRequired();

			builder.HasOne(f => f.Sticker)
				.WithMany()
				.HasForeignKey(f => f.StickerId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}