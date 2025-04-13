using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonManager.Application.Abstractions.Report;
using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.XUnit.src
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportService> _mockReportService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _mockReportService = new Mock<IReportService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ReportController(_mockReportService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetList_ReturnsOkResult_WhenReportsExist()
        {
            var reports = new List<ReportDto>
        {
            new ReportDto { Id = Guid.NewGuid(), ReportStatus=Common.Enums.ReportStatus.Completed },
            new ReportDto { Id = Guid.NewGuid(), ReportStatus=Common.Enums.ReportStatus.Completed }
        };

            _mockReportService.Setup(service => service.GetListAsync())
                .ReturnsAsync(reports);

            var result = await _controller.GetList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ReportDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenModelIsValid()
        {
            var reportRequest = new ReportRequestDto
            {
               ReportStatus=Common.Enums.ReportStatus.Preparing
            };

            var createdReport = new ReportDto { Id = Guid.NewGuid(), ReportStatus = Common.Enums.ReportStatus.Preparing };
            _mockReportService.Setup(service => service.CreateAsync(reportRequest))
                .ReturnsAsync(createdReport);

            var result = await _controller.Create(reportRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ReportDto>(okResult.Value);
            Assert.Equal(Common.Enums.ReportStatus.Preparing, returnValue.ReportStatus);
        }
    }
}
