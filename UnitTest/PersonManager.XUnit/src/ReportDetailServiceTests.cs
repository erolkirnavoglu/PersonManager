using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Mappings;
using PersonManager.Application.ReportDetail;
using PersonManager.Common.Enums;
using PersonManager.Domain;
using PersonManager.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.XUnit.src
{
    public class ReportDetailServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ReportDetailService _service;

        public ReportDetailServiceTests()
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

            _service = new ReportDetailService(_context, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldGenerateReportDetailsAndUpdateReport()
        {
            var personId1 = Guid.NewGuid();
            var personId2 = Guid.NewGuid();
            var location = "Ankara";

            _context.PersonInfos.AddRange(
                new PersonInfo { Id = Guid.NewGuid(), PersonId = personId1, ContactType = ContactType.Location, Content = location },
                new PersonInfo { Id = Guid.NewGuid(), PersonId = personId1, ContactType = ContactType.Phone, Content = "111" },
                new PersonInfo { Id = Guid.NewGuid(), PersonId = personId2, ContactType = ContactType.Location, Content = location },
                new PersonInfo { Id = Guid.NewGuid(), PersonId = personId2, ContactType = ContactType.Phone, Content = "222" }
            );

            var report = new Report
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ReportStatus = ReportStatus.Preparing
            };
            _context.Reports.Add(report);

            await _context.SaveChangesAsync();

            await _service.CreateAsync(report.Id);

            var updatedReport = await _context.Reports.FindAsync(report.Id);
            var reportDetails = await _context.ReportDetails.Where(p => p.ReportId == report.Id).ToListAsync();

            Assert.NotNull(updatedReport);
            Assert.Equal(ReportStatus.Completed, updatedReport.ReportStatus);
            Assert.Single(reportDetails);

            var detail = reportDetails.First();
            Assert.Equal(location, detail.Location);
            Assert.Equal(2, detail.PersonCount);
            Assert.Equal(2, detail.PhoneNumberCount);
        }

        [Fact]
        public async Task GetByReportDetailListAsync_ShouldReturnDetails()
        {
            var reportId = Guid.NewGuid();

            _context.ReportDetails.AddRange(
                new ReportDetail { Id = Guid.NewGuid(), ReportId = reportId, Location = "İzmir", PersonCount = 1, PhoneNumberCount = 1, CreatedDate = DateTime.UtcNow },
                new ReportDetail { Id = Guid.NewGuid(), ReportId = reportId, Location = "Ankara", PersonCount = 2, PhoneNumberCount = 3, CreatedDate = DateTime.UtcNow }
            );

            await _context.SaveChangesAsync();

            var result = await _service.GetByReportDetailListAsync(reportId);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Location == "İzmir");
            Assert.Contains(result, r => r.Location == "Ankara");
        }

    }
}
