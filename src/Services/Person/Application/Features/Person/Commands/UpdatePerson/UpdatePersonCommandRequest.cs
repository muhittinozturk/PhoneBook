using Domain.Entities;
using MediatR;

namespace Application.Features.Person.Commands.UpdatePerson
{
    public class UpdatePersonCommandRequest : IRequest<UpdatePersonCommandResponse>
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
