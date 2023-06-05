using MediatR;

namespace ContactManager.Application.UseCases.DeleteContact
{
    public class DeleteContactRequest : IRequest<VoidResponse>
    {
        public Guid PersonId { get; set; }
        public Guid Id { get; set; }
    }
}
