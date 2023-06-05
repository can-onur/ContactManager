using MassTransit;
using Report.Application.Common;
using Report.Application.UseCases;
using Report.Application.UseCases.PrepareReport;

namespace Report.Application.Messaging.Publisher
{
    public class PrepareReportPublisher : IPrepareReportPublisher
    {
        private readonly IBus _bus;
        public PrepareReportPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task<VoidResponse> Publish(PrepareReportRequest item, CancellationToken cancellationToken)
        {
            var queueEndPoint = new Uri($"exchange:{Constants.RabbitMQ.Queue.ReportRequest}");
            var publishEndPoint = await _bus.GetSendEndpoint(queueEndPoint);

            await publishEndPoint.Send(item, cancellationToken);

            return VoidResponse.Empty;
        }
    }
}
