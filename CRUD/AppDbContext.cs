using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Model
{
	public class AppDbContext : DbContext
	{
        public DbSet<Person> Persons { get; set; }
		public DbSet<Company> Companies { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
		{
			try
			{
				var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
				if (databaseCreator != null)
				{
					if (!databaseCreator.CanConnect()) databaseCreator.Create();

					if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

