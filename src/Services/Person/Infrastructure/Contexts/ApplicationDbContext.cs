using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
           .HasMany(p => p.Contacts)
           .WithOne(ci => ci.Person)
           .HasForeignKey(ci => ci.PersonId)
           .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
    
}
