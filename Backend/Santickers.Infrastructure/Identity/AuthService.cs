using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Santickers.Application.DTOs;
using Santickers.Application.Interfaces;
using Santickers.Infrastructure.Identity.Settings;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Santickers.Infrastructure.Identity
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtSettings _jwtSettings;

		public AuthService(
			UserManager<ApplicationUser> userManager,
			IOptions<JwtSettings> jwtSettings)
		{
			_userManager = userManager;
			_jwtSettings = jwtSettings.Value;
		}

		public async Task<RegisterResultDto> RegisterAsync(RegisterDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);

			var user = new ApplicationUser
			{
				UserName = dto.Email,
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName
			};

			var result = await _userManager.CreateAsync(user, dto.Password);

			if (!result.Succeeded)
			{
				return new RegisterResultDto
				{
					Succeeded = false,
					Errors = result.Errors.Select(e => e.Description).ToList()
				};
			}

			return new RegisterResultDto
			{
				Succeeded = true,
				Data = new AuthResponseDto
				{
					Token = GenerateJwtToken(user),
					ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
					Email = user.Email!
				}
			};
		}

		public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);

			var user = await _userManager.FindByEmailAsync(dto.Email);

			if (user is null)
				return null;

			var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

			if (!passwordValid)
				return null;

			return new AuthResponseDto
			{
				Token = GenerateJwtToken(user),
				ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
				Email = user.Email!
			};
		}

		private string GenerateJwtToken(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var credentials = new SigningCredentials(
				key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}