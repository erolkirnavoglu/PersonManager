using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Mappings;
using PersonManager.Application.Person;
using PersonManager.Persistence.Context;

namespace PersonManager.XUnit
{
    public class PersonServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly PersonService _personService;
        public PersonServiceTests()
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

            _personService = new PersonService(_context, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddPerson()
        {
            var model = new PersonRequestDto
            {
                Name = "Erol",
                Surname = "Kırnavoğlu",
                Company="KocSistem",
                 
            };

            var result = await _personService.CreateAsync(model);

            Assert.NotNull(result);
            Assert.Equal("Erol", result.Name);
            Assert.Equal("Kırnavoğlu", result.Surname);
            Assert.Equal("KocSistem", result.Company);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnAllPersons()
        {
            await _personService.CreateAsync(new PersonRequestDto { Name = "Ali", Surname = "Beyza",Company="Luna" });
            await _personService.CreateAsync(new PersonRequestDto { Name = "Ceylin", Surname = "Durmaz",Company="Çilek" });

            
            var list = await _personService.GetListAsync();

            
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePerson_WhenExists()
        {
            var created = await _personService.CreateAsync(new PersonRequestDto { Name = "Ali", Surname = "Fırtına",Company="X" });

            var result = await _personService.DeleteAsync(created.Id);

            Assert.True(result);
            var all = await _personService.GetListAsync();
            Assert.Empty(all);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            var result = await _personService.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }
        [Fact]
        public async Task EditAsync_ShouldUpdatePerson()
        {
            var created = await _personService.CreateAsync(new PersonRequestDto { Name = "Old", Surname = "Name",Company="FF" });

            var updated = await _personService.EditAsync(new PersonRequestDto
            {
                Id = created.Id,
                Name = "New",
                Surname = "Updated"
            });

            Assert.Equal("New", updated.Name);
            Assert.Equal("Updated", updated.Surname);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPerson_WhenExists()
        {
            var created = await _personService.CreateAsync(new PersonRequestDto { Name = "X", Surname = "Y",Company="Z" });

            var result = await _personService.GetByIdAsync(created.Id);

            Assert.NotNull(result);
            Assert.Equal("X", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var result = await _personService.GetByIdAsync(Guid.NewGuid());
            Assert.Null(result);
        }

    }   
}
