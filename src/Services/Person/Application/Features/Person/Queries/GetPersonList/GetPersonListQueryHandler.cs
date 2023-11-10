using Application.Abstract;
using Application.Features.Person.Queries.GetPerson;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Queries.GetPersonList
{
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQueryRequest, List<GetPersonListQueryResponse>>
    {
        private readonly IPersonService _personService;

        public GetPersonListQueryHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public Task<List<GetPersonListQueryResponse>> Handle(GetPersonListQueryRequest request, CancellationToken cancellationToken)
        {
            var persons = _personService.GetAll();

            var result = persons.Select(person => new GetPersonListQueryResponse
            {
                PersonId = person.UUID,
                Company = person.Company,
                FirstName = person.FirstName,
                LastName = person.LastName
            }).ToList();

            return Task.FromResult(result);
        }
    }

}
