using Application.Abstract;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQueryRequest, GetPersonQueryResponse>
    {
        private readonly IPersonService _personService;

        public GetPersonQueryHandler(IPersonService personService)
        {
            _personService = personService;
        }
        public async Task<GetPersonQueryResponse> Handle(GetPersonQueryRequest request, CancellationToken cancellationToken)
        {
            var person = await _personService.GetByIdAsync(request.PersonId);

            return new()
            {
                PersonId = person.UUID,
                Company = person.Company,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Contacts = person.Contacts,
            };
        }
    }
}
