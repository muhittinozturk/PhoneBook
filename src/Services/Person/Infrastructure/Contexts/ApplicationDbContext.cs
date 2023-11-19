using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
           .HasMany(person => person.Contacts)
           .WithOne(contact => contact.Person)
           .HasForeignKey(contact => contact.PersonId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
            .HasMany(r => r.ReportDetails)
            .WithOne(rd => rd.Report)
            .HasForeignKey(rd => rd.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
    
}
