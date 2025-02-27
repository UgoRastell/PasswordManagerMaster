using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.DTOs
{
	public class AddPasswordEntryRequestDto
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
