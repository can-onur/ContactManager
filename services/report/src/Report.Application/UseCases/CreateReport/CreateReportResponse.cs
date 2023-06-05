using Report.Domain.Shared.Enums;

namespace Report.Application.UseCases.CreateReport
{
    public class CreateReportResponse
    {
        public Guid Id { get; set; }
        public ReportStatus Status { get; set; }
    }
}
