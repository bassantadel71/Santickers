using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.Id);

			builder.Property(o => o.FullName).IsRequired().HasMaxLength(150);
			builder.Property(o => o.Email).IsRequired().HasMaxLength(150);
			builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(30);
			builder.Property(o => o.StreetAddress).IsRequired().HasMaxLength(300);
			builder.Property(o => o.City).IsRequired().HasMaxLength(100);
			builder.Property(o => o.Governorate).IsRequired().HasMaxLength(100);
			builder.Property(o => o.PostalCode).IsRequired().HasMaxLength(20);

			builder.Property(o => o.Subtotal).HasPrecision(18, 2);
			builder.Property(o => o.ShippingFee).HasPrecision(18, 2);
			builder.Property(o => o.Total).HasPrecision(18, 2);

			builder.Property(o => o.PaymobOrderId).HasMaxLength(100);
			builder.Property(o => o.PaymobTransactionId).HasMaxLength(100);

			builder.HasMany(o => o.Items)
				.WithOne(i => i.Order)
				.HasForeignKey(i => i.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
