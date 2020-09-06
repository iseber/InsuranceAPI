using Insurance.Api.Config;
using Microsoft.Extensions.Options;
using Moq;

namespace Insurance.Tests
{
    public class MockHelper
    {
        public static IOptions<AppSettings> AppSettings()
        {
            var mockAppSettings = new Mock<IOptions<AppSettings>>();
            mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings
            {
                AllowedHosts = "*",
                ProductApi = "http://localhost:5002"
            });
            return mockAppSettings.Object;
        }
    }
}