using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;
using ContactManager.Domain.ValueObjects;
using MediatR;

namespace ContactManager.Application.UseCases.CreateContact
{
    public class CreateContactUseCase : IRequestHandler<CreateContactRequest, CreateContactResponse>
    {
        private readonly IPersonRepository _personRepository;

        public CreateContactUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<CreateContactResponse> Handle(CreateContactRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);

            if (person.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            var contact = new Contact(Guid.Empty, request.PersonId, request.Type, request.Value);

            person.AddContact(contact);

            var result = await _personRepository?.UpdateAsync(person);

            var response = new CreateContactResponse
            {
                Id = result.Contacts.Last().Id,
            };

            return response;
        }
    }
}