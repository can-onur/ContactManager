using MassTransit;
using Report.Application.UseCases.PrepareReport;

namespace Report.Infrastructure.Messaging.Consumer
{
    public interface ILocationReportConsumer
    {
        public Task Consume(ConsumeContext<PrepareReportRequest> context);
    }
}
