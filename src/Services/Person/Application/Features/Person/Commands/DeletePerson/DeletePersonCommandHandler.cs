using Application.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommandRequest, DeletePersonCommandResponse>
    {
        private readonly IPersonService _personService;

        public DeletePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<DeletePersonCommandResponse> Handle(DeletePersonCommandRequest request, CancellationToken cancellationToken)
        {
            await _personService.DeleteAsync(request.Id);
            await _personService.SaveAsync(cancellationToken);
            return new()
            {
                IsSuccess = true,
            };
        }
    }
}
