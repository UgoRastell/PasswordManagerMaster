using System.ComponentModel.DataAnnotations;

namespace ProjetMaster.Core.DTOs
{
	public class LoginRequestDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
