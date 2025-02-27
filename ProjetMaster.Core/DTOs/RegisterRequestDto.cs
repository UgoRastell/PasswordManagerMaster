using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.DTOs
{
	public class RegisterRequestDto
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
		public string Password { get; set; }
	}

}
