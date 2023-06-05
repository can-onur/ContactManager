using MediatR;

namespace ContactManager.Application.UseCases.GetPerson
{
    public class GetPersonRequest : IRequest<GetPersonResponse>
    {
        public Guid Id { get; set; }
    }
}
