using Report.Domain.Aggregates.ReportAggregate;
using Report.Domain.Common;
using Report.Infrastructure.Common;
using Report.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Report.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILocationReportRepository, LocationReportRepository>();
            services.AddMongo();
            services.AddMongoRepository();
        }
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializationProvider(new GuidSerializationProvider());
            BsonSerializer.RegisterSerializationProvider(new DateTimeOffsetSerializationProvider());

            services.AddSingleton(o =>
            {
                var configuration = o.GetService<IConfiguration>();
                var mongoConfiguration = MongoConfiguration.FromConfiguration(configuration);

                var settings = MongoClientSettings.FromConnectionString(mongoConfiguration.ConnectionString);
                var mongoClient = new MongoClient(settings);

                return mongoClient.GetDatabase(mongoConfiguration.DatabaseName);
            });

            return services;
        }
        public static IServiceCollection AddMongoRepository(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<LocationInformationReport>>(o =>
            {
                var database = o.GetService<IMongoDatabase>();
                return new LocationReportRepository(database);
            });

            return services;
        }
        private class GuidSerializationProvider : IBsonSerializationProvider
        {
            public IBsonSerializer? GetSerializer(Type type)
            {
                if (type == typeof(Guid))
                    return new GuidSerializer(BsonType.String);

                return null;
            }
        }
        private class DateTimeOffsetSerializationProvider : IBsonSerializationProvider
        {
            public IBsonSerializer? GetSerializer(Type type)
            {
                if (type == typeof(DateTimeOffset))
                    return new DateTimeOffsetSerializer(BsonType.String);

                return null;
            }
        }
    }
}
