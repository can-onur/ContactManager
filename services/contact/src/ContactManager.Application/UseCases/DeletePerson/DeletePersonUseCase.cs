using ContactManager.Domain.Aggregates.PersonAggregate;
using MediatR;

namespace ContactManager.Application.UseCases.DeletePerson
{
    public class DeletePersonUseCase : IRequestHandler<DeletePersonRequest, VoidResponse>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<VoidResponse> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
        {
            await _personRepository.DeleteAsync(request.Id);

            return VoidResponse.Empty;
        }
    }
}