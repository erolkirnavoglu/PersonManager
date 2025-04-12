using PersonManager.Common.Enums;

namespace PersonManager.Application.Abstractions.Report.Contracts
{
    public class ReportDto
    {
        public Guid Id { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
