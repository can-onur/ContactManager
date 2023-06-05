using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Infrastructure.Common;
using ContactManager.Infrastructure.Persistence;
using ContactManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services,
        IConfiguration configuration)
        {

            services.AddDbContext<ContactManagerDbContext>(options =>

            options.UseNpgsql(PostgresConfiguration.FromConfiguration(configuration).ConnectionString));

            services.AddScoped<IContactManagerDbContext, ContactManagerDbContext>();

            return services;
        }

        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddPostgres(configuration);
        }
    }
}
