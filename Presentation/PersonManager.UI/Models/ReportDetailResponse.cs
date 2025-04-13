namespace PersonManager.UI.Models
{
    public class ReportDetailResponse
    {
        public Guid Id { get; set; }

        public Guid ReportId { get; set; }

        public string Location { get; set; }

        public int PersonCount { get; set; }

        public int PhoneNumberCount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
