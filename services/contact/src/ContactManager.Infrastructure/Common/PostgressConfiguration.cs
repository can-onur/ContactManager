using Microsoft.Extensions.Configuration;

namespace ContactManager.Infrastructure.Common
{
    public class PostgresConfiguration
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $"Server={Server};Port={Port};Database={Database};User Id = {UserName}; Password={Password}";

        public static PostgresConfiguration FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("PostgresConfiguration").Get<PostgresConfiguration>();
        }
    }
}
