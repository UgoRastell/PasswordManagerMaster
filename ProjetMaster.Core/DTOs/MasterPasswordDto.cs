using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.DTOs
{
	public class MasterPasswordDto
	{
		[Required]
		public string MasterPassword { get; set; }
	}
}
