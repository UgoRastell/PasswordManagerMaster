using System.ComponentModel.DataAnnotations;

namespace ProjetMaster.Core.DTOs
{
	public class UserUpdateRequestDto
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; }



	}
}
