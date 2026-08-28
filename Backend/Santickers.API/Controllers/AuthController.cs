using Microsoft.AspNetCore.Mvc;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;

namespace Santickers.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
		{
			var result = await _authService.RegisterAsync(dto);

			if (!result.Succeeded)
				return BadRequest(new { errors = result.Errors });

			return Ok(result.Data);
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
		{
			var result = await _authService.LoginAsync(dto);

			if (result is null)
				return Unauthorized();

			return Ok(result);
		}
	}
}