using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;
using MediatR;

namespace ContactManager.Application.UseCases.GetPerson
{
    public class GetPersonUseCase : IRequestHandler<GetPersonRequest, GetPersonResponse>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<GetPersonResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
           
            if (person.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            var response = new GetPersonResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Company = person.Company,
                Contacts = person.Contacts,
            };

            return response;

        }
    }
}
