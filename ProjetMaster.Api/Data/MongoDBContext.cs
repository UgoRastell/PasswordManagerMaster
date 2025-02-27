   using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Models;
using ProjetMaster.Core.Interfaces;
using ProjetMaster.Core.Models;
using ProjetMaster.Core.Models.ProjetMaster.Core.Models;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
       {
       }

	public DbSet<PasswordEntry> PasswordEntries { get; set; }
	public DbSet<UsersModel> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
}
   