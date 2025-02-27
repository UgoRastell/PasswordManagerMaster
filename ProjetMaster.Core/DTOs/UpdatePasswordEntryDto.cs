using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.DTOs
{
	public class UpdatePasswordEntryDto
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string EncryptedPassword { get; set; }
		public string Notes { get; set; }
		public Guid? CategoryId { get; set; }
		[Required]
		public Guid UserId { get; set; }
	}

}
