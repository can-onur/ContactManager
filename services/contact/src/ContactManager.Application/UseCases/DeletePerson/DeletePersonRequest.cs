using ContactManager.Application.UseCases.CreatePerson;
using MediatR;

namespace ContactManager.Application.UseCases.DeletePerson
{
    public class DeletePersonRequest : IRequest<VoidResponse>
    {
        public Guid Id { get; set; }
    }
}
