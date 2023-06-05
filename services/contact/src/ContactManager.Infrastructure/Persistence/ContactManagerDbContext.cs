using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Persistence
{
    public class ContactManagerDbContext : DbContext, IContactManagerDbContext
    {
        public DbSet<PersonRecord> Persons { get; set; }
        public DbSet<ContactRecord> Contacts { get; set; }

        public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
