using Report.Domain.Common;
using Report.Domain.Common.Guards;
using Report.Domain.Exceptions;
using Report.Domain.Shared.Enums;

namespace Report.Domain.Aggregates.ReportAggregate
{
    public class LocationInformationReport: IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public ReportStatus Status { get; private set; } = ReportStatus.Created;
        public List<LocationReport> LocationReports { get; set; }

        public void AddLocationReport(LocationReport locationReport)
        {
            locationReport.GuardAgainstEmpty(ValidationMessages.LocationReportEmpty);

            LocationReports.Add(locationReport);
        }

        public void RemoveLocationReport(LocationReport locationReport)
        {
            locationReport.GuardAgainstEmpty(ValidationMessages.LocationReportEmpty);

            LocationReports.Remove(locationReport);
        }

        public void MarkAsCompleted()
        {
            if (Status != ReportStatus.Completed)
            {
                Status = ReportStatus.Completed;
            }
        }
    }
}
