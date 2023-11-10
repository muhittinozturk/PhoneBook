using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Person.Queries.GetPersonList
{
    public class GetPersonListQueryRequest : IRequest<List<GetPersonListQueryResponse>>
    {
    }
}
