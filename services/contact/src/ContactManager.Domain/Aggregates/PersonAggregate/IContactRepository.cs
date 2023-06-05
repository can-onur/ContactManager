using ContactManager.Domain.Common;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Domain.Aggregates.PersonAggregate
{
    public interface IContactRepository : IRepository<Contact>
    {
    }
}
