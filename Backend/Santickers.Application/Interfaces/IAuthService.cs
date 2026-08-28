using Santickers.Application.DTOs;

namespace Santickers.Application.Interfaces
{
	public interface IAuthService
	{
		Task<RegisterResultDto> RegisterAsync(RegisterDto dto);

		Task<AuthResponseDto?> LoginAsync(LoginDto dto);
	}
}