using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Santickers.Domain.Entities;
using Santickers.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Persistence.Data
{
	public class ApplicationDbContext : IdentityDbContext<
		ApplicationUser,
		IdentityRole<Guid>,
		Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Sticker> Stickers { get; set; }

		public DbSet<Favorite> Favorites { get; set; }
	}
}