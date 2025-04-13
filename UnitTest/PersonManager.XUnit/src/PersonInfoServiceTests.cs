using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;
using PersonManager.Application.Mappings;
using PersonManager.Application.PersonInfo;
using PersonManager.Persistence.Context;

namespace PersonManager.XUnit.src
{
    public class PersonInfoServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly PersonInfoService _personInfoService;

        public PersonInfoServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
            _personInfoService = new PersonInfoService(_context, _mapper);
        }
        [Fact]
        public async Task CreateAsync_ShouldAddPersonInfo()
        {
            
            var person = new Domain.Person
            {
                Id = Guid.NewGuid(),
                Name = "Ali",
                Surname = "Demir",
                Company = "Test"
            };
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var model = new PersonInfoRequestDto
            {
                PersonId = person.Id,
                ContactType = Common.Enums.ContactType.Email,
                Content = "ali@test.com"
            };

            
            var result = await _personInfoService.CreateAsync(model);

            Assert.NotNull(result);
            Assert.Equal("ali@test.com", result.Content);
        }

        [Fact]
        public async Task GetPersonIdInfoListAsync_ShouldReturnInfos()
        {
            var person = new Domain.Person
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = "Person",
                Company = "ABC"
            };
            _context.Persons.Add(person);

            _context.PersonInfos.AddRange(
                new Domain.PersonInfo { Id = Guid.NewGuid(), PersonId = person.Id, ContactType = Common.Enums.ContactType.Phone, Content = "123" },
                new Domain.PersonInfo { Id = Guid.NewGuid(), PersonId = person.Id, ContactType = Common.Enums.ContactType.Location, Content = "Istanbul" }
            );
            await _context.SaveChangesAsync();

            var result = await _personInfoService.GetPersonIdInfoListAsync(person.Id);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemovePersonInfo_WhenExists()
        {
            var person = new Domain.Person { Id = Guid.NewGuid(), Name = "Ali", Surname = "Veli", Company = "X" };
            var info = new Domain.PersonInfo { Id = Guid.NewGuid(), PersonId = person.Id, ContactType = Common.Enums.ContactType.Phone, Content = "12345" };

            _context.Persons.Add(person);
            _context.PersonInfos.Add(info);
            await _context.SaveChangesAsync();

            var result = await _personInfoService.DeleteAsync(info.Id);

            Assert.True(result);
            Assert.Empty(await _context.PersonInfos.ToListAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            var result = await _personInfoService.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }

    }
}
