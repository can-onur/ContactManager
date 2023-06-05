using Report.Application;
using Report.Infrastructure;

namespace Report.Api
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
