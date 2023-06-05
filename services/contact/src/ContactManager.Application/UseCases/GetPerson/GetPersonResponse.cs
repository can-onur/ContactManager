using ContactManager.Domain.ValueObjects;

namespace ContactManager.Application.UseCases.GetPerson
{
    public class GetPersonResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
