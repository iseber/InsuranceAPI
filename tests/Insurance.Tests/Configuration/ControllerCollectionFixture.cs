using Xunit;

namespace Insurance.Tests.Configuration
{
    [CollectionDefinition("Controller collection")]
    public class ControllerCollectionFixture : ICollectionFixture<ControllerTestFixture>
    {
        
    }
}