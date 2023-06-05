using ContactManager.Domain.Common;
using ContactManager.Domain.Shared;

namespace ContactManager.Domain.ValueObjects
{
    public class Contact : ValueObject, IEntity
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public ContactType Type { get; private set; }
        public string Value { get; private set; }

        public Contact(Guid id, Guid personId, ContactType type, string value)
        {
            Id = id;
            Type = type;
            Value = value;
            PersonId = personId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }
    }
}
