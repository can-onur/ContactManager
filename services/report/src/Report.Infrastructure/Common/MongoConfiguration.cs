using Microsoft.Extensions.Configuration;

namespace Report.Infrastructure.Common
{
    public class MongoConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString => $"mongodb://{UserName}:{Password}@{Host}:{Port}";

        public static MongoConfiguration FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("MongoConfiguration").Get<MongoConfiguration>();
        }
    }
}
