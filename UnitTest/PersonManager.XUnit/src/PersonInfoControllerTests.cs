using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonManager.Application.Abstractions.PersonInfo;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;
using PersonManager.WebAPI.Controllers;

namespace PersonManager.XUnit.src
{
    public class PersonInfoControllerTests
    {
        private readonly Mock<IPersonInfoService> _mockPersonInfoService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PersonInfoController _controller;

        public PersonInfoControllerTests()
        {
            _mockPersonInfoService = new Mock<IPersonInfoService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new PersonInfoController(_mockPersonInfoService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetPersonIdInfo_ReturnsOkResult_WhenPersonExists()
        {
            var personId = Guid.NewGuid();
            var id = Guid.NewGuid();
            var personInfos = new List<PersonInfoDto>
        {
            new PersonInfoDto {Id=id ,PersonId = personId, ContactType=Common.Enums.ContactType.Location}
        };

            _mockPersonInfoService.Setup(service => service.GetPersonIdInfoListAsync(personId))
                .ReturnsAsync(personInfos);


            var result = await _controller.GetPersonIdInfo(personId);


            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PersonInfoDto>>(okResult.Value);
            Assert.Equal(1, returnValue.Count);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenModelIsValid()
        {
            var personInfoRequest = new PersonInfoRequestDto
            {
                ContactType= Common.Enums.ContactType.Location,
                Content="izmir"
            };

            var createdPerson = new PersonInfoDto { PersonId = Guid.NewGuid(), Content="izmir" };
            _mockPersonInfoService.Setup(service => service.CreateAsync(personInfoRequest))
                .ReturnsAsync(createdPerson);

            
            var result = await _controller.Create(personInfoRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PersonInfoDto>(okResult.Value);
            Assert.Equal("izmir", returnValue.Content);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenPersonIsDeleted()
        {
            var personId = Guid.NewGuid();
            _mockPersonInfoService.Setup(service => service.DeleteAsync(personId));

            var result = await _controller.Delete(personId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.False(returnValue);
        }
    }
}
