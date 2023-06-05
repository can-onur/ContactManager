using ContactManager.Domain.Aggregates.PersonAggregate;
using MediatR;

namespace ContactManager.Application.UseCases.CreatePerson
{
    public class CreatePersonUseCase : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = PersonBuilder.Person()
                .WithFirstName(request.FirstName)
                .WithLastName(request.LastName)
                .WithCompany(request.Company)
                .Build();

            var result = await _personRepository.AddAsync(person);

            var response = new CreatePersonResponse
            {
                Id = result.Id,
            };

            return response;
        }
    }
}