using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.Report;
using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.Persistence.Context;

namespace PersonManager.Application.Report
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReportDto>> GetListAsync()
        {
            var reports = await _context.Reports.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<ReportDto>>(reports);
        }

        public async Task<ReportDto> CreateAsync(ReportRequestDto model)
        {
            var report = _mapper.Map<Domain.Report>(model);
            report.CreatedDate = DateTime.UtcNow;
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReportDto>(report);
        }
    }
}
