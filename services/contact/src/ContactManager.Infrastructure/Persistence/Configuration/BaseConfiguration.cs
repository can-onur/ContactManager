using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    protected abstract void DoConfigure(EntityTypeBuilder<TEntity> builder);

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        DoConfigure(builder);
    }
}