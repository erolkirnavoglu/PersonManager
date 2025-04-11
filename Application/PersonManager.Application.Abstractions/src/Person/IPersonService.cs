using PersonManager.Application.Abstractions.Person.Contracts;

namespace PersonManager.Application.Abstractions.Person
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetListAsync();

        Task<PersonDto> GetByIdAsync(Guid id);

        Task<PersonDto> CreateAsync(PersonRequestDto model);

        Task<PersonDto> EditAsync(PersonRequestDto model);

        Task<bool> DeleteAsync(Guid id);
    }
}
