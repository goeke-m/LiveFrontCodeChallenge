using System.Net.Http;

namespace CartonCaps.Tests.WebApi;

[TestFixture]
[Category(TestCategories.Integration)]
public class HealthCheckTests : TestsWithTestContainer
{
    [Test]
    public async Task GetApiHealth_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync("api/health");

        // Assert
        httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }
}
