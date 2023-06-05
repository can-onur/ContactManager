using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;
using MediatR;

namespace ContactManager.Application.UseCases.DeleteContact
{
    public class DeleteContactUseCase : IRequestHandler<DeleteContactRequest, VoidResponse>
    {
        private readonly IPersonRepository _personRepository;

        public DeleteContactUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<VoidResponse> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            var person =  await _personRepository.GetByIdAsync(request.PersonId);

            if (person.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            var contact = person.Contacts.FirstOrDefault(x => x.Id == request.Id);

            if (contact.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.ContactNotFound);
            }

            person.RemoveContact(contact);

            _personRepository?.UpdateAsync(person);

            return VoidResponse.Empty;
        }
    }
}