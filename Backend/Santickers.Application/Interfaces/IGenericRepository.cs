using Santickers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T>> GetAllAsync();

		Task<T?> GetByIdAsync(int id);

		Task AddAsync(T entity);

		void Update(T entity);

		void Delete(T entity);
		Task<IEnumerable<T>> GetAllReadOnlyAsync(params Expression<Func<T, object?>>[] includes);
		Task<T?> GetByIdReadOnlyAsync(int id, params Expression<Func<T, object?>>[] includes);
	}
}
