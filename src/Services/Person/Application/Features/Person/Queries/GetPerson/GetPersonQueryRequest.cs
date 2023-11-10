using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Queries.GetPerson
{
    public class GetPersonQueryRequest : IRequest<GetPersonQueryResponse>
    {
        public Guid PersonId { get; set; }
    }
}
