using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonManager.Application.Abstractions.Report;
using PersonManager.Application.Abstractions.Report.Contracts;
using System.Net.Mime;

namespace PersonManager.WebAPI.Controllers
{
    [Route("reports")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var result = await _reportService.GetListAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportRequestDto model)
        {
            var result = await _reportService.CreateAsync(model);
            return Ok(result);
        }
    }
}
