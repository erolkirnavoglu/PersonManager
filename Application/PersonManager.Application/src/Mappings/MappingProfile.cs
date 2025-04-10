using AutoMapper;
using PersonManager.Application.Abstractions.Person.Contracts;


namespace PersonManager.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Person, PersonDto>();
            CreateMap<PersonRequestDto, Domain.Person>();
        }
    }
}
