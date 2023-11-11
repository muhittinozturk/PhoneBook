using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Person.Commands.AddPerson
{
    public class AddPersonCommandRequest : IRequest<AddPersonCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<CreatePersonContactDto> Contacts { get; set; }
    }
}
