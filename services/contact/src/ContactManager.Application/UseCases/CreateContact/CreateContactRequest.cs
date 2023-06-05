using ContactManager.Domain.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace ContactManager.Application.UseCases.CreateContact
{
    public class CreateContactRequest : IRequest<CreateContactResponse>
    {
        [JsonIgnore]
        public Guid PersonId { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }
}
