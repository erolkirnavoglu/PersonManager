using PersonManager.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManager.Domain
{
    public class ReportDetail : IEntity
    {
        public Guid Id { get; set; }

        public Guid ReportId { get; set; }

        public string Location { get; set; }

        public int PersonCount { get; set; }

        public int PhoneNumberCount { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
    }
}
