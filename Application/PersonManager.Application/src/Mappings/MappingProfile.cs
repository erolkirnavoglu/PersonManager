using AutoMapper;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;
using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.Application.Abstractions.ReportDetail.Contracts;


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
            CreateMap<Domain.Report, ReportDto>();
            CreateMap<ReportRequestDto, Domain.Report>();
            CreateMap<Domain.ReportDetail, ReportDetailDto>().ReverseMap();
        }
    }
}
