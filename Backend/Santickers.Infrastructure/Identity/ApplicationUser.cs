using Microsoft.AspNetCore.Identity;

namespace Santickers.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }
	}
}