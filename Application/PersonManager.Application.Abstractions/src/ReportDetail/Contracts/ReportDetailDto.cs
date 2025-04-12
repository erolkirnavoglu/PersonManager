namespace PersonManager.Application.Abstractions.ReportDetail.Contracts
{
    public class ReportDetailDto
    {
        public Guid Id { get; set; }

        public Guid ReportId { get; set; }

        public string Location { get; set; }

        public int PersonCount { get; set; }

        public int PhoneNumberCount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
