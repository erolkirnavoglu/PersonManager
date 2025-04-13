using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonManager.Application.Abstractions.ReportDetail;
using PersonManager.Application.Abstractions.ReportDetail.Contracts;
using PersonManager.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.XUnit.src
{
    public class ReportDetailControllerTests
    {
        private readonly Mock<IReportDetailService> _mockReportDetailService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportDetailController _controller;

        public ReportDetailControllerTests()
        {
            _mockReportDetailService = new Mock<IReportDetailService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ReportDetailController(_mockReportDetailService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetByReportDetailList_ReturnsOkResult_WhenDetailsExist()
        {
           
            var reportId = Guid.NewGuid();
            var reportDetails = new List<ReportDetailDto>
        {
            new ReportDetailDto { Id = Guid.NewGuid(), ReportId = reportId, PersonCount=2 },
            new ReportDetailDto { Id = Guid.NewGuid(), ReportId = reportId, PersonCount=2 }
        };

            _mockReportDetailService.Setup(service => service.GetByReportDetailListAsync(reportId))
                .ReturnsAsync(reportDetails);

            var result = await _controller.GetByReportDetailList(reportId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ReportDetailDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetByReportDetailList_ReturnsNotFoundResult_WhenNoDetailsExist()
        {
            
            var reportId = Guid.NewGuid();
            _mockReportDetailService.Setup(service => service.GetByReportDetailListAsync(reportId))
                .ReturnsAsync(new List<ReportDetailDto>());

            
            var result = await _controller.GetByReportDetailList(reportId);

            
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var returnValue = okResult?.Value as List<ReportDetailDto>;
            Assert.Empty(returnValue);
        }
    }
}
