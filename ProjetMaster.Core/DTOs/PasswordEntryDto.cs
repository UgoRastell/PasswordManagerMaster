using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetMaster.Core.DTOs
{
	public class PasswordEntryDto
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public string Notes { get; set; }
		public Guid? CategoryId { get; set; }
	}
}
