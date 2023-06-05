using ContactManager.Domain.Shared;

namespace ContactManager.Domain.Entities
{
    public class ContactRecord : BaseEntity
    {
        public Guid PersonId { get; set; }
        public ContactType Type { get; set; }
        public string Value{ get; set; }
        public virtual PersonRecord Person { get; set; }
    }
}
