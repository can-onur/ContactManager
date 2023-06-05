using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Infrastructure.Persistence.Configurations;

public class ContactConfiguration : BaseEntityTypeConfiguration<ContactRecord>
{
    protected override void DoConfigure(EntityTypeBuilder<ContactRecord> builder)
    {
        builder.Property(t => t.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Type)
            .IsRequired();

        builder.Property(t => t.PersonId)
            .IsRequired();
    }
}