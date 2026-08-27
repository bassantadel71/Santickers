using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.Interfaces
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
