using MediatR;
using Report.Application.Messaging.Publisher;
using Report.Application.UseCases.PrepareReport;
using Report.Domain.Aggregates.ReportAggregate;
using Report.Domain.Shared.Enums;

namespace Report.Application.UseCases.CreateReport
{
    public class CreateReportUseCase : IRequestHandler<CreateReportRequest, CreateReportResponse>
    {
        private readonly ILocationReportRepository _locationReportRepository;
        private readonly IPrepareReportPublisher _prepareReportPublisher;

        public CreateReportUseCase(ILocationReportRepository locationReportRepository, IPrepareReportPublisher prepareReportPublisher)
        {
            _locationReportRepository = locationReportRepository;
            _prepareReportPublisher = prepareReportPublisher;
        }

        public async Task<CreateReportResponse> Handle(CreateReportRequest request, CancellationToken cancellationToken)
        {
            var locationReport = new LocationInformationReport();

            await _locationReportRepository.AddAsync(locationReport);

            var prepareReportRequest = new PrepareReportRequest() { ReportId = locationReport.Id };

            await _prepareReportPublisher.Publish(prepareReportRequest, cancellationToken);

            return new CreateReportResponse() { Id = locationReport.Id, Status = ReportStatus.Created };
        }
    }
}