using ContactManager.Domain.Shared;

namespace ContactManager.Application.UseCases.GetAllContacts
{
    public class GetAllContactsResponse
    {
        public Guid PersonId { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }
}
