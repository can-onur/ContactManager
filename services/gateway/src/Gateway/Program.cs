using Gateway.Configuration;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.ConfigureOcelot().ConfigureOcelotPipeline().Run();
        }
    }
}