using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.Application.Common;
using Report.Application.Messaging.Consumer;
using Report.Application.Messaging.Publisher;
using Report.Application.UseCases.PrepareReport;
using Report.Infrastructure.Messaging.Consumer;

namespace Report.Application.Messaging
{
    public static class MessagingModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.Add(configuration);
        }

        private static void Add(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitmqConfiguration = RabbitMQConfiguration.FromConfiguration(configuration);

            services.AddTransient<IPrepareReportPublisher, PrepareReportPublisher>();
            services.AddTransient<ILocationReportConsumer, LocationReportConsumer>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(rabbitmqConfiguration.Host, rabbitmqConfiguration.VirtualHost, h => {
                        h.Username(rabbitmqConfiguration.UserName);
                        h.Password(rabbitmqConfiguration.Password);
                    });

                    config.MessageTopology.SetEntityNameFormatter(new NameFormatter());
                    config.ReceiveEndpoint(Constants.RabbitMQ.Queue.ReportRequest, e =>
                    {
                        e.Lazy = true;
                        e.PrefetchCount = 100;
                        e.Bind<PrepareReportRequest>();
                        e.Consumer(() => new LocationReportConsumer(services.BuildServiceProvider()));
                    });
                    config.ConfigureEndpoints(context);
                });
            });
        }
    }
}
