using Report.Domain.Shared.Enums;

namespace Report.Application.UseCases.GetAllReports
{
    public class GetAllReportsResponse
    {
        public List<LocationInformationReport> locationInformationReports { get; set; }
    }

    public class LocationInformationReport
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportStatus Status { get; set; }
    }
}
