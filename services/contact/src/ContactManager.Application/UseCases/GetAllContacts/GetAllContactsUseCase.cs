using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Domain.Exceptions;
using ContactManager.Domain.Shared.Extensions;
using MediatR;

namespace ContactManager.Application.UseCases.GetAllContacts
{

    public class GetAllContactsUseCase : IRequestHandler<GetAllContactsRequest, IEnumerable<GetAllContactsResponse>>
    {
        private readonly IContactRepository _contactRepository;

        public GetAllContactsUseCase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<GetAllContactsResponse>> Handle(GetAllContactsRequest request, CancellationToken cancellationToken)
        {
            var results = await _contactRepository.GetAllAsync();

            if (results.IsEmpty())
            {
                throw new NotFoundException(NotFoundMessages.PersonNotFound);
            }

            var response = results.Select(result => new GetAllContactsResponse
            {
                PersonId = result.PersonId,
                Type = result.Type,
                Value = result.Value,
            });

            return response;

        }
    }
}
