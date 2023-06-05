using Microsoft.Extensions.Configuration;

namespace Report.Application.Common
{
    public class RabbitMQConfiguration
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }

        public static RabbitMQConfiguration FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
        }
    }
}
