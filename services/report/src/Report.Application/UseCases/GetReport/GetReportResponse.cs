using Report.Domain.Shared.Enums;

namespace Report.Application.UseCases.GetReport
{
    public class GetReportResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportStatus Status { get; set; }
        public List<LocationReport> LocationReport { get; set; }
    }

    public class LocationReport
    {
        public string Location { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfPhoneNumbers { get; set; }
    }
}
