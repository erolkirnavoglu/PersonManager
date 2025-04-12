using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.PersonInfo;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;
using PersonManager.Domain;
using PersonManager.Persistence.Context;

namespace PersonManager.Application.PersonInfo
{
    public class PersonInfoService : IPersonInfoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PersonInfoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PersonInfoDto> CreateAsync(PersonInfoRequestDto model)
        {
            var personInfo = _mapper.Map<Domain.PersonInfo>(model);
            personInfo.CreatedDate = DateTime.UtcNow;
            _context.PersonInfos.Add(personInfo);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonInfoDto>(personInfo);
        }

        public async Task<List<PersonInfoDto>> GetPersonIdInfoListAsync(Guid personId)
        {
            var personInfos = await _context.PersonInfos.Where(p => p.PersonId == personId).ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<PersonInfoDto>>(personInfos);
        }
    }
}
