using Application.Features.Person.Commands.AddPerson;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.PersonMap
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, AddPersonCommandRequest>().ReverseMap();
        }
    }
}
