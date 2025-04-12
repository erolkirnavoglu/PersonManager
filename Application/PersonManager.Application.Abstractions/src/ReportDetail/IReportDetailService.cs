using PersonManager.Application.Abstractions.Report.Contracts;
using PersonManager.Application.Abstractions.ReportDetail.Contracts;

namespace PersonManager.Application.Abstractions.ReportDetail
{
    public interface IReportDetailService
    {
        Task CreateAsync(Guid reportId);
    }
}
