using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Persistence.Context;

namespace PersonManager.Application.Person
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PersonService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonDto> CreateAsync(PersonRequestDto model)
        {
            var person = _mapper.Map<Domain.Person>(model);
            person.CreatedDate = DateTime.UtcNow;
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonDto>(person);

        }
        public async Task<List<PersonDto>> GetListAsync()
        {
            var persons = await _context.Persons.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<PersonDto>>(persons);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await _context.Persons.Include(p=>p.PersonInfos).FirstOrDefaultAsync(x => x.Id == id);
            if (person is not null)
            {
                _context.PersonInfos.RemoveRange(person.PersonInfos);
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PersonDto> GetByIdAsync(Guid id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> EditAsync(PersonRequestDto model)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == model.Id);
            var entity = _mapper.Map(model, person);
            _context.Persons.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonDto>(person);
        }
    }
}
