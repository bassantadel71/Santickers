using Santickers.Application.Interfaces;
using Santickers.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
