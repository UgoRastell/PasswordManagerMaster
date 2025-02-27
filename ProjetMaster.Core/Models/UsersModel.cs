using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	namespace ProjetMaster.Core.Models
	{
		public class UsersModel
		{
			public Guid Id { get; set; }
			public string Email { get; set; }
			public string MotDePasse { get; set; }
			public int FailedDecryptionAttempts { get; set; }
			public DateTime DateInscription { get; set; }
			public DateTime? LockoutEndTime { get; set; }
		}
	}

}
