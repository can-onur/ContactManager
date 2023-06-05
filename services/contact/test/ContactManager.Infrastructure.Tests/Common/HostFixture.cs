using Microsoft.Extensions.Hosting;

namespace ContactManager.Infrastructure.Tests.Common
{
    public class HostFixture : IDisposable
    {
        public HostFixture()
        {
            var args = new string[] { };
            HostInstance = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => services.Register(hostContext.Configuration))
                .Build();
        }

        public IHost HostInstance { get; }

        public void Dispose()
        {
            HostInstance.Dispose();
        }
    }
}
