using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Data
{
	public class DataContext : DbContext

	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public DbSet<Students> Students { get; set; }
		public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to many relationship
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Gender)              // Student to gender relationship
                .WithMany()                         // Gender can have many students
                .HasForeignKey(s => s.GenderId);    // Students have a foreign key which is GenderIds
        }
    }
}


