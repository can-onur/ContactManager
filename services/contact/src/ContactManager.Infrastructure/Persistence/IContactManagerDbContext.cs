using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Persistence
{
    public interface IContactManagerDbContext
    {
        DbSet<PersonRecord> Persons { get; set; }
        DbSet<ContactRecord> Contacts { get; set; }
    }
}
