using PersonManager.Common.Enums;
using PersonManager.Domain.Base;

namespace PersonManager.Domain
{
    public class Report:IEntity
    {
        public Guid Id { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ReportDetail> ReportDetails { get; set; }
    }
}
