using PersonManager.Application.Abstractions.Person.Contracts;

namespace PersonManager.Application.Abstractions.Person
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetListAsync();

        Task<PersonDto> CreateAsync(PersonRequestDto model);
    }
}
