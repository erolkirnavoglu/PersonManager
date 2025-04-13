using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonManager.Application.Abstractions.Report;
using PersonManager.Application.Abstractions.ReportDetail;
using PersonManager.Application.Report;
using System.Net.Mime;

namespace PersonManager.WebAPI.Controllers
{
    [Route("report-details")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReportDetailController : ControllerBase
    {
        private readonly IReportDetailService _reportDetailService;
        private readonly IMapper _mapper;
        public ReportDetailController(IReportDetailService reportDetailService, IMapper mapper)
        {
            _reportDetailService = reportDetailService;
            _mapper = mapper;
        }

        [HttpGet("detail/{reportId}")]
        public async Task<IActionResult> GetByReportDetailList(Guid reportId)
        {
            var details = await _reportDetailService.GetByReportDetailListAsync(reportId);
            return Ok(details);
        }
    }
}
