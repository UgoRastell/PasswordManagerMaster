using Microsoft.AspNetCore.Mvc;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Services;

namespace ProjetMaster.Api.Controllers.Auth
{
	[ApiController]
	[Route("api/[controller]")]
	public class RegisterController : ControllerBase
	{
		private readonly AuthService _authService;

		public RegisterController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterRequestDto request)
		{
			var (success, message) = _authService.Register(request);
			if (!success)
				return Conflict(new { message });

			return Created("api/register", new { message });
		}
	}
}
