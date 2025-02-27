using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Interfaces;
using ProjetMaster.Core.Models;
using ProjetMaster.Core.Models.ProjetMaster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetMaster.Core.Services
{
	public class UserService
	{
		private readonly IApplicationDbContext _context;
		private const int MAX_DECRYPTION_ATTEMPTS = 3;
		private readonly TimeSpan LOCKOUT_DURATION = TimeSpan.FromSeconds(5);

		public UserService(IApplicationDbContext context)
		{
			_context = context;
		}

		public UsersModel GetUserById(Guid id)
		{
			return _context.Users.Find(id);
		}

		public List<UsersModel> GetAllUsers()
		{
			return _context.Users.ToList();
		}

		public (bool Success, string Message) UpdateUser(Guid id, UserUpdateRequestDto request)
		{
			var user = _context.Users.Find(id);

			if (user == null)
				return (false, "Utilisateur introuvable.");

			user.Email = request.Email;

			_context.Users.Update(user);
			_context.SaveChanges();

			return (true, "Profil mis à jour avec succès.");
		}

		public (bool Success, string Message) DeleteUser(Guid id)
		{
			var user = _context.Users.Find(id);

			if (user == null)
				return (false, "Utilisateur introuvable.");

			_context.Users.Remove(user);
			_context.SaveChanges();

			return (true, "Utilisateur supprimé avec succès.");
		}

		public bool IsUserLocked(Guid userId)
		{
			var user = _context.Users.Find(userId);
			if (user == null)
				return false; 

			if (user.LockoutEndTime.HasValue)
			{
				if (user.LockoutEndTime.Value <= DateTime.UtcNow)
				{
					user.FailedDecryptionAttempts = 0;
					user.LockoutEndTime = null;
					_context.Users.Update(user);
					_context.SaveChanges();
					return false;
				}
				else
				{
					// Le compte est toujours verrouillé
					return true;
				}
			}
			// Aucun verrouillage en place
			return false;
		}


		// Incrémente le compteur de tentatives et verrouille si nécessaire
		public (bool IsLocked, string Message) IncrementDecryptionAttempts(Guid userId)
		{
			var user = _context.Users.Find(userId);
			if (user == null)
				return (false, "Utilisateur introuvable.");

			// Vérifier si l'utilisateur est déjà verrouillé
			if (user.LockoutEndTime.HasValue && user.LockoutEndTime.Value > DateTime.UtcNow)
				return (true, "Compte déjà verrouillé.");

			user.FailedDecryptionAttempts++;

			if (user.FailedDecryptionAttempts >= MAX_DECRYPTION_ATTEMPTS)
			{
				user.LockoutEndTime = DateTime.UtcNow.Add(LOCKOUT_DURATION);
				_context.Users.Update(user);
				_context.SaveChanges();
				return (true, "Compte verrouillé suite à plusieurs tentatives échouées. Veuillez réessayer plus tard.");
			}

			_context.Users.Update(user);
			_context.SaveChanges();
			return (false, "Tentative échouée. Veuillez vérifier votre mot de passe principal.");
		}

		// Réinitialise le compteur de tentatives en cas de succès
		public void ResetDecryptionAttempts(Guid userId)
		{
			var user = _context.Users.Find(userId);
			if (user != null)
			{
				user.FailedDecryptionAttempts = 0;
				user.LockoutEndTime = null;
				_context.Users.Update(user);
				_context.SaveChanges();
			}
		}
	}
}
