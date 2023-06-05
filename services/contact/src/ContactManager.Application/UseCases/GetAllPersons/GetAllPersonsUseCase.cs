using ContactManager.Application.UseCases.GetAllPersons;
using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;
using MediatR;

namespace ContactManager.Application.UseCases.GetPerson
{
    public class GetAllPersonsUseCase : IRequestHandler<GetAllPersonsRequest, IEnumerable<GetAllPersonsResponse>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonsUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<GetAllPersonsResponse>> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
        {
            var results = await _personRepository.GetAllAsync();

            if (results.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            var response = results.Select(result => new GetAllPersonsResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Company = result.Company,
                Contacts = result.Contacts,
            });

            return response;

        }
    }
}
