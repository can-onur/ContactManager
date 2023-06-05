using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneBook.Contact.Infrastructure.Persistence.Configurations;

public class PersonConfiguration : BaseEntityTypeConfiguration<PersonRecord>
{
    protected override void DoConfigure(EntityTypeBuilder<PersonRecord> builder)
    {
        builder.Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Company)
            .HasMaxLength(100)
            .IsRequired();
    }
}