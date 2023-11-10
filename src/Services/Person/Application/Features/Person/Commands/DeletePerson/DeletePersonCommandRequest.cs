using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Commands.DeletePerson
{
    public class DeletePersonCommandRequest : IRequest<DeletePersonCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
