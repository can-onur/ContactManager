

using Microsoft.EntityFrameworkCore;
using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Exceptions;
using System;
using ContactManager.Domain.Shared.Extensions;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ContactManagerDbContext _dbContext;
        private readonly DbSet<PersonRecord> _dbSet;

        public PersonRepository(ContactManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<PersonRecord>();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            var record = await _dbSet.Include(x => x.Contacts).FirstOrDefaultAsync(x=> x.Id == id);

            if (record == null) { return null; }

            return Get(record);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var records = await _dbSet.Include(x=> x.Contacts).ToListAsync();

            return records.Select(record => Get(record)).ToList();
        }

        public virtual async Task<Person> AddAsync(Person entity)
        {
            var record = Create(entity);

            await _dbSet.AddAsync(record);

            await _dbContext.SaveChangesAsync(default);

            return Get(record);
        }

        public virtual async Task<Person> UpdateAsync(Person entity)
        {
            var record = await _dbSet.Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Id == entity.Id); ;

            if (record == null) { return null; }

            record.FirstName = entity.FirstName;
            record.LastName = entity.LastName;
            record.Company = entity.Company;
            record.Contacts = entity.Contacts?.Select(contact => new ContactRecord() { PersonId = contact.PersonId, Person = record , Type = contact.Type, Value = contact.Value }).ToList() ?? new List<ContactRecord>();

            _dbSet.Update(record);

            await _dbContext.SaveChangesAsync(default);

            return Get(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            if (entity == null) { return; }

            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync(default);
        }

        private PersonRecord Create(Person person)
        {
            return new PersonRecord
            {
                Id = person.Id == Guid.Empty ? Guid.NewGuid() : person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Company = person.Company,
                Contacts = person.Contacts?.Select(contact => new ContactRecord() { Type = contact.Type, Value = contact.Value }).ToList() ?? new List<ContactRecord>()
            };
        }

        private Person Get(PersonRecord record)
        {
            var contacts = record.Contacts?.Select(contact => new Contact(contact.Id, contact.PersonId, contact.Type, contact.Value)).ToList() ?? new List<Contact>();

            return PersonBuilder.Person()
                .WithId(record.Id)
                .WithFirstName(record.FirstName)
                .WithLastName(record.LastName)
                .WithCompany(record.Company)
                .WithContacts(contacts)
                .Build();
        }
    }
}
