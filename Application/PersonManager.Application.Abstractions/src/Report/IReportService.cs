using PersonManager.Application.Abstractions.Person.Contracts;
using PersonManager.Application.Abstractions.Report.Contracts;

namespace PersonManager.Application.Abstractions.Report
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetListAsync();
        Task<ReportDto> CreateAsync(ReportRequestDto model);
    }
}
