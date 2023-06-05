using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.Application.Messaging;

namespace Report.Application
{
    public static class ApplicationModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddMediatR(typeof(MediatRModule));
            MessagingModule.Register(services, configuration);
        }
    }
}
