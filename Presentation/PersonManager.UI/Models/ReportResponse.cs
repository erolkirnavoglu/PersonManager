using PersonManager.Common.Enums;

namespace PersonManager.UI.Models
{
    public class ReportResponse
    {
        public Guid Id { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
