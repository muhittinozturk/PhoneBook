using Application.Abstract;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Person.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQueryRequest, GetPersonQueryResponse>
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public GetPersonQueryHandler(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }
        public async Task<GetPersonQueryResponse> Handle(GetPersonQueryRequest request, CancellationToken cancellationToken)
        {
            var person = await _personService.GetByIdAsync(request.PersonId, p => p.Contacts);
            if (person is null)
                throw new PersonNotFoundExcepiton("Kişi bilgisi bulunamadı");

            var response = _mapper.Map<GetPersonQueryResponse>(person);

            return response;

        }
    }
}
