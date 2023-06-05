using MediatR;

namespace Report.Application.UseCases.GetReport
{
    public class GetReportRequest : IRequest<GetReportResponse>
    {
        public Guid Id { get; set; }
    }
}
