using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.Application.Abstractions.ReportDetail.Contracts;

namespace PersonManager.Application.Abstractions.ReportDetail
{
    public interface IReportDetailService
    {
        Task<List<ReportDetailDto>> GetByReportDetailListAsync(Guid reportId);

        Task CreateAsync(Guid reportId);
    }
}
