using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Models;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Services;
using System;
using System.Linq;
using System.Security.Claims;

namespace ProjetMaster.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoriesController : ControllerBase
	{
		private readonly CategoryService _categoryService;

		public CategoriesController(CategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAllCategories()
		{
			// Récupérer l'ID de l'utilisateur connecté
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var categories = _categoryService.GetAllCategoriesForUser(userId);
			var categoryDtos = categories.Select(c => new CategoryDto
			{
				Id = c.Id,
				Name = c.Name
			}).ToList();
			return Ok(categoryDtos);
		}

		[HttpGet("{id}")]
		[Authorize]
		public IActionResult GetCategoryById(Guid id)
		{
			var category = _categoryService.GetCategoryById(id);
			if (category == null)
				return NotFound();

			var categoryDto = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name
			};
			return Ok(categoryDto);
		}

		[HttpPost]
		[Authorize]
		public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
		{
			// Récupérer l'ID de l'utilisateur connecté
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			var category = new Category
			{
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				UserId = userId
			};
			_categoryService.AddCategory(category);
			return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDto);
		}

		[HttpPut("{id}")]
		[Authorize]
		public IActionResult UpdateCategory(Guid id, [FromBody] CategoryDto categoryDto)
		{
			if (id != categoryDto.Id)
				return BadRequest();

			// Vous pouvez également vérifier que la catégorie appartient à l'utilisateur ici.

			var category = new Category
			{
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				// Le UserId ne doit pas être modifié par le client.
			};
			_categoryService.UpdateCategory(category);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult DeleteCategory(Guid id)
		{
			_categoryService.DeleteCategory(id);
			return NoContent();
		}
	}
}
