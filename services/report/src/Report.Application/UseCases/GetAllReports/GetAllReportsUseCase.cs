using MediatR;
using Report.Domain.Aggregates.ReportAggregate;

namespace Report.Application.UseCases.GetAllReports
{
    public class GetAllReportsUseCase : IRequestHandler<GetAllReportsRequest, GetAllReportsResponse>
    {
        private readonly ILocationReportRepository _locationReportRepository;

        public GetAllReportsUseCase(ILocationReportRepository locationReportRepository)
        {
            _locationReportRepository = locationReportRepository;
        }

        public async Task<GetAllReportsResponse> Handle(GetAllReportsRequest request, CancellationToken cancellationToken)
        {
            var reports = await _locationReportRepository.GetAllAsync();
            var response = new GetAllReportsResponse();
                response.locationInformationReports = reports
                                                .Select(x => new LocationInformationReport() { Id = x.Id, CreatedAt = x.CreatedAt, Status = x.Status })
                                                .ToList();

            return response;
        }
    }
}
