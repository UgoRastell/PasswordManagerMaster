using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Models;
using ProjetMaster.Core.Models;
using ProjetMaster.Core.Models.ProjetMaster.Core.Models;
using System.Collections.Generic;

namespace ProjetMaster.Core.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<UsersModel> Users { get; set; }
		DbSet<PasswordEntry> PasswordEntries { get; set; }
		DbSet<Category> Categories { get; set; }
		int SaveChanges();
	}
}
