using ContactManager.Domain.ValueObjects;

namespace ContactManager.Domain.Aggregates.PersonAggregate
{
    public class PersonBuilder
    {
        public static PersonBuilder Person()
        {
            return new PersonBuilder();
        }

        private string _firstName;
        private string _lastName;
        private string _company;
        private List<Contact> _contacts = new List<Contact>();
        private Guid _id = Guid.NewGuid();

        public PersonBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public PersonBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public PersonBuilder WithCompany(string company)
        {
            _company = company;
            return this;
        }

        public PersonBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PersonBuilder WithContacts(List<Contact> contacts)
        {
            _contacts = contacts;
            return this;
        }

        public Person Build()
        {
            var person = new Person(_id,_firstName, _lastName, _company);
            _contacts.ForEach(contact => person.AddContact(contact));
            return person;
        }
    }
}
