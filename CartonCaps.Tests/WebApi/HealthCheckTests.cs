namespace CartonCaps.Tests.WebApi;

public class HealthCheckTests : TestsWithTestContainer
{
    [Test]
    public async Task GetApiHealth_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync("api/v1/health");

        // Assert
        httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }
}
