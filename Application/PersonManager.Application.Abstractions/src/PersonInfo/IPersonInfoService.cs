using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;

namespace PersonManager.Application.Abstractions.PersonInfo
{
    public interface IPersonInfoService
    {
        Task<PersonInfoDto> CreateAsync(PersonInfoRequestDto model);

        Task<List<PersonInfoDto>> GetPersonIdInfoListAsync(Guid personId);
    }
}
