using PasswordManager.Core.Models;
using ProjetMaster.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetMaster.Core.Services
{
	public class CategoryService
	{
		private readonly IApplicationDbContext _context;

		public CategoryService(IApplicationDbContext context)
		{
			_context = context;
		}

		// Nouvelle méthode pour obtenir uniquement les catégories de l'utilisateur.
		public List<Category> GetAllCategoriesForUser(Guid userId)
		{
			return _context.Categories.Where(c => c.UserId == userId).ToList();
		}

		public Category GetCategoryById(Guid id)
		{
			return _context.Categories.Find(id);
		}

		public void AddCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void UpdateCategory(Category category)
		{
			var existingCategory = _context.Categories.Find(category.Id);
			if (existingCategory == null)
			{
				throw new Exception("Catégorie non trouvée.");
			}

			// Mise à jour du nom seulement (UserId ne doit pas être modifié)
			existingCategory.Name = category.Name;

			_context.Categories.Update(existingCategory);
			_context.SaveChanges();
		}


		public void DeleteCategory(Guid id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				// Récupérer tous les mots de passe ayant cette catégorie
				var passwordEntries = _context.PasswordEntries.Where(pe => pe.CategoryId == id).ToList();

				// Pour chacun, mettre CategoryId à null
				foreach (var entry in passwordEntries)
				{
					entry.CategoryId = null;
				}

				// Supprimer la catégorie
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}

	}
}
