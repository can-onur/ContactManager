using MediatR;

namespace ContactManager.Application.UseCases.CreatePerson
{
    public class CreatePersonRequest : IRequest<CreatePersonResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get;  set; }
    }
}
