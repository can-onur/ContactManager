using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Report.Application.ExternalServices.Persons;
using Report.Application.UseCases.PrepareReport;
using Report.Domain.Aggregates.ReportAggregate;
using Report.Domain.Shared.Enums;
using Report.Infrastructure.Messaging.Consumer;

namespace Report.Application.Messaging.Consumer
{
    public class LocationReportConsumer : IConsumer<PrepareReportRequest>, ILocationReportConsumer
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocationReportRepository _locationReportRepository;

        public LocationReportConsumer(IServiceProvider serviceProvider)
        {
            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            _locationReportRepository = serviceProvider.GetRequiredService<ILocationReportRepository>();
        }

        public async Task Consume(ConsumeContext<PrepareReportRequest> context)
        {
            var response = await new PersonHttpService(_httpClientFactory).GetAll();

            var result = response
                .Where(k => k.Type == ContactType.Location)
                .GroupBy(k => k.Value,
                (k, g) => new { Key = k, Contacts = g.ToList() })
                .Select(item => new LocationReport
                {
                    Location = item.Key,
                    NumberOfPeople = item.Contacts.Count(),
                    NumberOfPhoneNumbers = response.Where(x=> x.Type == ContactType.PhoneNumber && item.Contacts.Select(z=> z.PersonId).Contains(x.PersonId)).Count()
                }).ToList();

            var report = await _locationReportRepository.GetByIdAsync(context.Message.ReportId);

            report.LocationReports = result;
            report.MarkAsCompleted();

            await _locationReportRepository.UpdateAsync(report);

            Console.WriteLine(report);
        }
    }
}
