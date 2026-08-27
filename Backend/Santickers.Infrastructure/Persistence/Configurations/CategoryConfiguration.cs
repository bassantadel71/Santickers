using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(c => c.Description)
				.HasMaxLength(500);

			builder.Property(c => c.ImageUrl)
				.HasMaxLength(500);

			builder.HasMany(c => c.Stickers)
				.WithOne(s => s.Category)
				.HasForeignKey(s => s.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
