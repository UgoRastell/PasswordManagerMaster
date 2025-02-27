using PasswordManager.Core.Models;
using ProjetMaster.Core.Helpers;
using ProjetMaster.Core.Interfaces;
using ProjetMaster.Core.Models.ProjetMaster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Core.Services
{
	public class PasswordService
	{
		private readonly IApplicationDbContext _context;

		public PasswordService(IApplicationDbContext context)
		{
			_context = context;
		}

		public List<PasswordEntry> GetAllPasswords(Guid userId)
		{
			return _context.PasswordEntries
				.Where(pe => pe.UserId == userId)
				.ToList();
		}

		public PasswordEntry GetPasswordById(Guid id)
		{
			return _context.PasswordEntries.Find(id);
		}

		public void AddPassword(PasswordEntry entry, string password)
		{
			const string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUV"; // 32 caractères
			entry.EncryptedPassword = EncryptionHelper.Encrypt(password, encryptionKey);
			_context.PasswordEntries.Add(entry);
			_context.SaveChanges();
		}






		public void UpdatePassword(PasswordEntry entry)
		{
			_context.PasswordEntries.Update(entry);
			_context.SaveChanges();
		}

		public void DeletePassword(Guid id)
		{
			var entry = _context.PasswordEntries.Find(id);
			if (entry != null)
			{
				_context.PasswordEntries.Remove(entry);
				_context.SaveChanges();
			}
		}
		public List<PasswordEntry> SearchPasswords(Guid userId, string title = null, Guid? categoryId = null)
		{
			// On démarre avec la requête sur les mots de passe de l'utilisateur
			var query = _context.PasswordEntries.AsQueryable().Where(pe => pe.UserId == userId);

			// Si un titre est spécifié, on filtre sur le champ Title (avec Contains pour une recherche partielle)
			if (!string.IsNullOrEmpty(title))
			{
				query = query.Where(pe => pe.Title.Contains(title));
			}

			// Si une catégorie est spécifiée, on filtre sur CategoryId
			if (categoryId.HasValue)
			{
				query = query.Where(pe => pe.CategoryId == categoryId.Value);
			}

			return query.ToList();
		}
	}
}
