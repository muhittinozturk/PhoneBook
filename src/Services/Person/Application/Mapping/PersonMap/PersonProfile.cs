using Application.Dtos;
using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Queries.GetPerson;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.PersonMap
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AddPersonCommandRequest, Person>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts));
            CreateMap<CreatePersonContactDto, Contact>();

            CreateMap<Person, GetPersonQueryResponse>()
           .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.UUID))
           .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts));

            CreateMap<Contact, GetPersonContactDto>()
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.UUID))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
        }
    
    }
}
