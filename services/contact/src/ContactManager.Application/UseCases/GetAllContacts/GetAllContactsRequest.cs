using MediatR;

namespace ContactManager.Application.UseCases.GetAllContacts
{
    public class GetAllContactsRequest : IRequest<IEnumerable<GetAllContactsResponse>>
    {
    }
}
