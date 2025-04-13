using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Domain;
using PersonManager.WebAPI.Controllers;

namespace PersonManager.XUnit.src
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new PersonController(_mockPersonService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetList_ReturnsOkResult_WithPersonList()
        {
            var mockPersonList = new List<Person>
        {
            new Person { Id = Guid.NewGuid(), Name = "John Doe" },
            new Person { Id = Guid.NewGuid(), Name = "Jane Doe" }
        };

            _mockPersonService.Setup(service => service.GetListAsync())
                  .ReturnsAsync(new List<PersonDto>
                  {
                      new PersonDto { Id = Guid.NewGuid(), Name = "John Doe" },
                      new PersonDto { Id = Guid.NewGuid(), Name = "Jane Doe" }
                  });


            var result = await _controller.GetList();


            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PersonDto>>(okResult.Value);
            Assert.Equal(mockPersonList.Count, returnValue.Count());
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedPerson()
        {
            var personRequest = new PersonRequestDto { Name = "John Doe" };
            var createdPerson = new Person { Id = Guid.NewGuid(), Name = "John Doe" };

            _mockPersonService.Setup(service => service.CreateAsync(personRequest))
                               .ReturnsAsync(new PersonDto
                               {
                                   Id = Guid.NewGuid(),
                                   Name = "John Doe"

                               });


            var result = await _controller.Create(personRequest);


            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PersonDto>(okResult.Value);
            Assert.Equal(createdPerson.Name, returnValue.Name);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithDeleteStatus()
        {
            var personId = Guid.NewGuid();
            var deleteStatus = true;

            _mockPersonService.Setup(service => service.DeleteAsync(personId))
                              .ReturnsAsync(deleteStatus);

            
            var result = await _controller.Delete(personId);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.Equal(deleteStatus, returnValue);
        }
        [Fact]
        public async Task GetInfo_ReturnsOkResult_WithPersonInfo()
        {
            var personId = Guid.NewGuid();
            var person = new Person { Id = personId, Name = "John Doe" };

            _mockPersonService.Setup(service => service.GetByIdAsync(personId))
                              .ReturnsAsync(new PersonDto
                              {
                                  Id = Guid.NewGuid(),
                                  Name = "John Doe"

                              });

            
            var result = await _controller.GetInfo(personId);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PersonDto>(okResult.Value);
            Assert.Equal(person.Name, returnValue.Name);
        }

        [Fact]
        public async Task Edit_ReturnsOkResult_WithUpdatedPerson()
        {
            var personRequest = new PersonRequestDto { Name = "John Doe" };
            var updatedPerson = new Person { Id = Guid.NewGuid(), Name = "John Doe" };

            _mockPersonService.Setup(service => service.EditAsync(personRequest))
                              .ReturnsAsync(new PersonDto
                              {
                                  Id = Guid.NewGuid(),
                                  Name = "John Doe"

                              });

            
            var result = await _controller.Edit(personRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PersonDto>(okResult.Value);
            Assert.Equal(updatedPerson.Name, returnValue.Name);
        }
    }
}
