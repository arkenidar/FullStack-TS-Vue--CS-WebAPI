
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Models
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }

    //public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer("Data Source=win-dc;Initial Catalog=BookLibrary;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
			
			optionsBuilder.UseSqlite("Data Source=books.db", b => b.MigrationsAssembly("FullStack-TS-Vue--CS-WebAPI.Server")); // DATABASE
		}
	}
	
	// DbContext Factory
    public class BookContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			
            optionsBuilder.UseSqlite("Data Source=books.db", b => b.MigrationsAssembly("FullStack-TS-Vue--CS-WebAPI.Server")); // DATABASE
			
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
