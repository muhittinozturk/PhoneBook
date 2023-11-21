using Application.Abstract;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Person.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommandRequest, UpdatePersonCommandResponse>
    {
        private readonly IPersonService _personService;

        public UpdatePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<UpdatePersonCommandResponse> Handle(UpdatePersonCommandRequest request, CancellationToken cancellationToken)
        {
            var person = await _personService.GetByIdAsync(request.PersonId);

            if (person is null)
                throw new PersonNotFoundExcepiton("Kişi bilgisi bulunamadı");

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Company = request.Company;

            var result = _personService.Update(person);

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
