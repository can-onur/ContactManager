using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure.Tests.Common
{
    public class BaseTest : IClassFixture<HostFixture>
    {
        private readonly HostFixture _fixture;

        public BaseTest(HostFixture fixture)
        {
            _fixture = fixture;
        }

        public T GetService<T>() where T : notnull
        {
            return _fixture.HostInstance.Services.GetRequiredService<T>();
        }
    }
}