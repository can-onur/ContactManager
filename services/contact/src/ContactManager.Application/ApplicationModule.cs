using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Application
{
    public static class ApplicationModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(MediatRModule));
        }
    }
}
