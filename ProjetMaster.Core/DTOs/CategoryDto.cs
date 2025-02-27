using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetMaster.Core.DTOs
{
	public class CategoryDto
	{
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
