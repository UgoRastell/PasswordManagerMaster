using Microsoft.AspNetCore.Mvc;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Services;

namespace ProjetMaster.Api.Controllers.Auth
{
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		private readonly AuthService _authService;

		public LoginController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginRequestDto request)
		{
			var (success, token, message, user) = _authService.Login(request.Email, request.Password);
			if (!success)
				return Unauthorized(new { message });

			return Ok(new { token, user });
		}
	}
}
