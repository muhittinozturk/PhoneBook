using Application.Abstract;
using AutoMapper;
using MediatR;
namespace Application.Features.Person.Commands.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommandRequest, AddPersonCommandResponse>
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public AddPersonCommandHandler(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        public async Task<AddPersonCommandResponse> Handle(AddPersonCommandRequest request, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<Domain.Entities.Person>(request);

            var result = await _personService.AddAsync(person);

            if (result)
            {
                await _personService.SaveAsync(cancellationToken);
                return new()
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new()
                {
                    IsSuccess = false
                };
            }

        }
    }
}
