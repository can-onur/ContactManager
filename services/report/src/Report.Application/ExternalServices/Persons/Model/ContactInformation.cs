using Report.Domain.Shared.Enums;

namespace Report.Application.ExternalServices.Persons.Model
{
    public class ContactInformation
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid PersonId { get; set; }
    }
}
