using System;
using System.Collections.Generic;
using System.Text;

namespace Santickers.Application.DTOs
{
	public class RegisterDto
	{
		public string Email { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public string? FirstName { get; set; }

		public string? LastName { get; set; }
	}

	public class LoginDto
	{
		public string Email { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;
	}

	public class AuthResponseDto
	{
		public string Token { get; set; } = string.Empty;

		public DateTime ExpiresAt { get; set; }

		public string Email { get; set; } = string.Empty;
	}

	public class RegisterResultDto
	{
		public bool Succeeded { get; set; }

		public AuthResponseDto? Data { get; set; }

		public List<string> Errors { get; set; } = new List<string>();
	}
}