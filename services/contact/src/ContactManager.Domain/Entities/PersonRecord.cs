namespace ContactManager.Domain.Entities
{
    public class PersonRecord : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public virtual ICollection<ContactRecord> Contacts { get; set; }
    }
}
