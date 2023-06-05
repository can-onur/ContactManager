using Report.Application.UseCases;
using Report.Application.UseCases.PrepareReport;

namespace Report.Application.Messaging.Publisher
{
    public interface IPrepareReportPublisher
    {
        public Task<VoidResponse> Publish(PrepareReportRequest item, CancellationToken cancellationToken);
    }
}
