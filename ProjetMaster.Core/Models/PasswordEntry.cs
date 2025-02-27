using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Core.Models
{
	public class PasswordEntry
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string EncryptedPassword { get; set; }

		public string Notes { get; set; }

		// Clé étrangère pour la catégorie
		public Guid? CategoryId { get; set; }
		public Category Category { get; set; }
		public Guid UserId { get; set; }
	}
}
