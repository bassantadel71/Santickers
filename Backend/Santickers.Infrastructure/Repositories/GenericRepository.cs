using Microsoft.EntityFrameworkCore;
using Santickers.Application.Interfaces;
using Santickers.Domain.Entities;
using Santickers.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T>
	where T : BaseEntity
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}


		// Read-only methods
		// AsNoTrackingAsync
		public async Task<IEnumerable<T>> GetAllReadOnlyAsync()
		{
			return await _dbSet
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<T?> GetByIdReadOnlyAsync(int id)
		{
			return await _dbSet
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
