using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Models;
using ProjetMaster.Core.Services;
using System.Collections.Generic;

namespace ProjetMaster.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly UserService _userService;

		public UsersController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet("{id}")]
		[Authorize]
		public IActionResult GetUserById(Guid id)
		{
			var user = _userService.GetUserById(id);

			if (user == null)
				return NotFound(new { message = "Utilisateur introuvable." });

			return Ok(user);
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAllUsers()
		{
			var users = _userService.GetAllUsers();
			return Ok(users);
		}

		[HttpPut("{id}")]
		[Authorize]
		public IActionResult UpdateUser(Guid id, [FromBody] UserUpdateRequestDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var (success, message) = _userService.UpdateUser(id, request);

			if (!success)
				return NotFound(new { message });

			return Ok(new { message });
		}

		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult DeleteUser(Guid id)
		{
			var (success, message) = _userService.DeleteUser(id);

			if (!success)
				return NotFound(new { message });

			return Ok(new { message });
		}

		
	}
}
