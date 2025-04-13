using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.Application.Mappings;
using PersonManager.Application.Report;
using PersonManager.Persistence.Context;
using PersonManager.RabbitMQ.Abstract;
using PersonManager.RabbitMQ.Concreate;

namespace PersonManager.XUnit.src
{
    public class ReportServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IRabbitMqPublisher> _mockRabbit;
        private readonly ReportService _reportService;

        public ReportServiceTests()
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
            _mockRabbit = new Mock<IRabbitMqPublisher>();
            _reportService = new ReportService(_context, _mockRabbit.Object, _mapper);
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnAllReports()
        {
            
            _context.Reports.AddRange(
                new Domain.Report { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow.AddMinutes(-10), ReportStatus = Common.Enums.ReportStatus.Completed },
                new Domain.Report { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, ReportStatus = Common.Enums.ReportStatus.Preparing }
            );
            await _context.SaveChangesAsync();

            
            var result = await _reportService.GetListAsync();

            
            Assert.Equal(2, result.Count);
            Assert.True(result[0].CreatedDate >= result[1].CreatedDate);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddReport_AndPublishMessage()
        {
            
            var model = new ReportRequestDto{};

            var result = await _reportService.CreateAsync(model);

            Assert.NotNull(result);
            Assert.Equal(Common.Enums.ReportStatus.Preparing, result.ReportStatus);
            Assert.NotEqual(Guid.Empty, result.Id);

            var reportInDb = await _context.Reports.FirstOrDefaultAsync(r => r.Id == result.Id);
            Assert.NotNull(reportInDb);

            _mockRabbit.Verify(r => r.Publish(It.Is<ReportRequestMessage>(m => m.ReportId == result.Id)), Times.Once);
        }

    }
}
