using ContactManager.Application.UseCases.GetPerson;
using MediatR;

namespace ContactManager.Application.UseCases.GetAllPersons
{
    public class GetAllPersonsRequest : IRequest<IEnumerable<GetAllPersonsResponse>>
    {
    }
}
