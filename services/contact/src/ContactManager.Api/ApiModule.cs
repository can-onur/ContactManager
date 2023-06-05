using ContactManager.Application;
using ContactManager.Infrastructure;

namespace ContactManager.Api
{
    public static class ApiModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            InfrastructureModule.Register(services, configuration);
            ApplicationModule.Register(services, configuration);
        }
    }
}
