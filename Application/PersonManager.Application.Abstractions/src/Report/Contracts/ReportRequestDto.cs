using PersonManager.Common.Enums;

namespace PersonManager.Application.Abstractions.Report.Contracts
{
    public class ReportRequestDto
    {
        public Guid Id { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
