using ProjetMaster.Core.Models.ProjetMaster.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Core.Models
{
	public class Category
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		// Collection des mots de passe liés à cette catégorie.
		public ICollection<PasswordEntry> PasswordEntries { get; set; }

		// Propriétés pour lier la catégorie à un utilisateur.
		[Required]
		public Guid UserId { get; set; }
		public UsersModel User { get; set; }
	}
}
