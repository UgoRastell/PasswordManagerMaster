using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Models;
using PasswordManager.Core.Services;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Services;
using System;
using System.Security.Claims;

namespace ProjetMaster.Api.Controllers.Auth
{
	[ApiController]
	[Route("api/[controller]")]
	public class PasswordsController : ControllerBase
	{
		private readonly PasswordService _passwordService;
		private readonly UserService _userService;

		public PasswordsController(PasswordService passwordService, UserService userService)
		{
			_passwordService = passwordService;
			_userService = userService;
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAll()
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var passwords = _passwordService.GetAllPasswords(userId);
			return Ok(passwords);
		}

		[HttpGet("{id}")]
		[Authorize]
		public IActionResult GetById(Guid id)
		{
			var password = _passwordService.GetPasswordById(id);
			if (password == null)
				return NotFound();
			return Ok(password);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Create([FromBody] AddPasswordEntryRequestDto entryDto)
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var user = _userService.GetUserById(userId);
			if (user == null)
				return NotFound("Utilisateur non trouvé.");

			var entry = new PasswordEntry
			{
				Title = entryDto.Title,
				Username = entryDto.Username,
				Notes = entryDto.Notes,
				CategoryId = entryDto.CategoryId,
				UserId = userId
			};

			// Appelle la méthode modifiée qui n'utilise plus de masterPassword
			_passwordService.AddPassword(entry, entryDto.Password);
			return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
		}



		[HttpPut("{id}")]
		[Authorize]
		public IActionResult Update(Guid id, [FromBody] UpdatePasswordEntryDto updateDto)
		{
			if (id != updateDto.Id)
				return BadRequest("L'ID ne correspond pas.");

			var existingEntry = _passwordService.GetPasswordById(id);
			if (existingEntry == null)
				return NotFound("Mot de passe introuvable.");

			existingEntry.Title = updateDto.Title;
			existingEntry.Username = updateDto.Username;
			existingEntry.Notes = updateDto.Notes;
			existingEntry.CategoryId = updateDto.CategoryId;

			_passwordService.UpdatePassword(existingEntry);
			return NoContent();
		}



		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult Delete(Guid id)
		{
			_passwordService.DeletePassword(id);
			return NoContent();
		}

		[HttpGet("{id}/plaintext")]
		[Authorize] // Vérifie automatiquement le JWT
		public IActionResult GetPlainTextPassword(Guid id)
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var entry = _passwordService.GetPasswordById(id);

			if (entry == null)
				return NotFound(new { message = "Entrée non trouvée." });

			if (entry.UserId != userId)
				return Forbid("Accès interdit à cette entrée.");

			try
			{
				const string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
				var decryptedPassword = ProjetMaster.Core.Helpers.EncryptionHelper.Decrypt(entry.EncryptedPassword, encryptionKey);
				return Ok(new { password = decryptedPassword });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = $"Erreur de déchiffrement : {ex.Message}" });
			}
		}





		/// <summary>
		/// Recherche rapide des mots de passe par titre et/ou catégorie.
		/// Exemples d'appel : 
		/// GET api/passwords/search?title=banque
		/// GET api/passwords/search?categoryId=xxxx-xxxx-xxxx-xxxx
		/// GET api/passwords/search?title=mail&categoryId=xxxx-xxxx-xxxx-xxxx
		/// </summary>
		[HttpGet("search")]
		[Authorize]
		public IActionResult Search([FromQuery] string title, [FromQuery] Guid? categoryId)
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var results = _passwordService.SearchPasswords(userId, title, categoryId);
			return Ok(results);
		}

	}
}
