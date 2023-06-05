

using Microsoft.EntityFrameworkCore;
using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Entities;
using ContactManager.Domain.ValueObjects;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;

namespace ContactManager.Infrastructure.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactManagerDbContext _dbContext;
        private readonly DbSet<ContactRecord> _dbSet;

        public ContactRepository(ContactManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ContactRecord>();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            var record = await _dbSet.FindAsync(id);

            if (record == null) { return null; }

            return Get(record);
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            var records = await _dbSet.ToListAsync();

            return records.Select(record => Get(record)).ToList();
        }

        public virtual async Task<Contact> AddAsync(Contact entity)
        {
            var record = Create(entity);

            await _dbSet.AddAsync(record);

            await _dbContext.SaveChangesAsync(default);

            return Get(record);
        }

        public virtual async Task<Contact> UpdateAsync(Contact entity)
        {
            var record = await _dbSet.FindAsync(entity.Id);

            if (record == null) { return null; }

            record.Type = entity.Type;
            record.Value = entity.Value;

            _dbSet.Update(record);

            await _dbContext.SaveChangesAsync(default);

            return Get(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.ContactNotFound);
            }

            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync(default);
        }

        private ContactRecord Create(Contact person)
        {
            return new ContactRecord
            {
                Id = person.Id == Guid.Empty ? Guid.NewGuid() : person.Id,
                Type = person.Type,
                Value = person.Value,
                PersonId = person.Id
            };
        }

        private Contact Get(ContactRecord record)
        {
            return new Contact(record.Id, record.PersonId,record.Type, record.Value);
        }
    }
}
