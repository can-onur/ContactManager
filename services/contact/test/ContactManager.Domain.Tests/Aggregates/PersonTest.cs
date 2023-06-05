using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Domain.Tests.Aggregates
{
    public class PersonTest
    {
        [Fact]
        public void Should_throw_exception_given_empty_firstName()
        {
            Action action = () => GetDefaultPersonBuilder()
                .WithFirstName(default(string))
                .Build();

            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void Should_throw_exception_given_empty_lastName()
        {
            Action action = () => GetDefaultPersonBuilder()
                .WithLastName(default(string))
                .Build();

            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void Should_throw_exception_given_empty_company()
        {
            Action action = () => GetDefaultPersonBuilder()
                .WithCompany(default(string))
                .Build();

            Assert.Throws<ValidationException>(action);
        }

        [Fact]
        public void AddContact_Should_AddContactToList()
        {
            var person = GetDefaultPersonBuilder().Build();

            var contact = new Contact(Guid.NewGuid(), Guid.NewGuid(), ContactType.PhoneNumber, "1234567890");

            person.AddContact(contact);

            Assert.Contains(contact, person.Contacts);
        }

        [Fact]
        public void RemoveContact_Should_RemoveContactFromList()
        {
            var person = GetDefaultPersonBuilder().Build();

            var contact = new Contact(Guid.NewGuid(), Guid.NewGuid(), ContactType.PhoneNumber, "1234567890");

            person.AddContact(contact);

            person.RemoveContact(contact);

            Assert.DoesNotContain(contact, person.Contacts);
        }

        private PersonBuilder GetDefaultPersonBuilder()
        {
            return PersonBuilder.Person()
               .WithFirstName("Onur")
               .WithLastName("Can")
               .WithCompany("TEI");
        }
    }
}