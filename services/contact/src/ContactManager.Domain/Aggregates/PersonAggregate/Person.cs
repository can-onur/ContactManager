using ContactManager.Domain.Common;
using ContactManager.Domain.Common.Guards;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Domain.Aggregates.PersonAggregate
{
    public class Person : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Company { get; private set; }
        public List<Contact> Contacts { get; private set; }
        public Person(string firstName, string lastName, string company)
        {
            FirstName = firstName.GuardAgainstNullOrWhiteSpace(ValidationMessages.FirstNameEmpty);
            LastName = lastName.GuardAgainstNullOrWhiteSpace(ValidationMessages.LastNameEmpty);
            Company = company.GuardAgainstNullOrWhiteSpace(ValidationMessages.CompanyEmpty);
            Contacts = new List<Contact>();
        }

        public Person(Guid id,string firstName, string lastName, string company)
        {
            Id = id;
            FirstName = firstName.GuardAgainstNullOrWhiteSpace(ValidationMessages.FirstNameEmpty);
            LastName = lastName.GuardAgainstNullOrWhiteSpace(ValidationMessages.LastNameEmpty);
            Company = company.GuardAgainstNullOrWhiteSpace(ValidationMessages.CompanyEmpty);
            Contacts = new List<Contact>();
        }

        public void SetCompany(string company)
        {
            Company = company;
        }

        public void AddContact(Contact contact)
        {
            contact.GuardAgainstEmpty(ValidationMessages.ContactEmpty);

            Contacts.GuardAgainstDuplicate(contact, ValidationMessages.DuplicateContact);

            Contacts.Add(contact);
        }

        public void RemoveContact(Contact contact)
        {
            contact.GuardAgainstEmpty(ValidationMessages.ContactEmpty);

            Contacts.Remove(contact);
        }
    }
}
