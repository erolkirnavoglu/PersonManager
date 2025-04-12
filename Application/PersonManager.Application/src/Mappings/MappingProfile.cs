using AutoMapper;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;


namespace PersonManager.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Person, PersonDto>();
            CreateMap<PersonRequestDto, Domain.Person>();
            CreateMap<Domain.PersonInfo, PersonInfoDto>();
            CreateMap<PersonInfoRequestDto, Domain.PersonInfo>();
        }
    }
}
