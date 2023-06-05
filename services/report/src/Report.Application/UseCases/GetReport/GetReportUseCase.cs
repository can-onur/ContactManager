using MediatR;
using Report.Domain.Aggregates.ReportAggregate;
using Report.Domain.Exceptions;
using Report.Domain.Shared.Enums;

namespace Report.Application.UseCases.GetReport
{
    public class GetReportUseCase : IRequestHandler<GetReportRequest, GetReportResponse>
    {
        private readonly ILocationReportRepository _locationReportRepository;

        public GetReportUseCase(ILocationReportRepository locationReportRepository)
        {
            _locationReportRepository = locationReportRepository;
        }

        public async Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {
            var report = await _locationReportRepository.GetByIdAsync(request.Id);
            var response = new GetReportResponse
            {
                Id = report.Id,
                Status = report.Status,
                CreatedAt = report.CreatedAt,
                LocationReport = report.LocationReports.Select(locationreport => new LocationReport() {
                    Location = locationreport.Location,
                    NumberOfPeople = locationreport.NumberOfPeople,
                    NumberOfPhoneNumbers = locationreport.NumberOfPhoneNumbers
                }).ToList()
            };

            if(response.Status == ReportStatus.Created)
            {
                throw new NotFoundException(NotFoundMessages.LocationReportNotFound);
            }

            return response;
        }
    }
}
