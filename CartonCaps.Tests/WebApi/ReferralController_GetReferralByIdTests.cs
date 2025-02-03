using System.Net;

namespace CartonCaps.Tests.WebApi;

public class ReferralController_GetReferralByIdTests : TestsWithTestContainer
{
    [Test]
    public async Task GetReferralById_WhenReferralExist_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferralbyid/{referral.Id}");

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task GetReferralById_WhenReferralExist_ReturnsReferral()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferralbyid/{referral.Id}");
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetReferralByIdResponse>(httpResponseString);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Referral.ShouldNotBeNull();
        response.Referral.ReferralCode.ShouldBe(referralCode);
        response.Referral.ReferralStatus.ShouldBe(referral.ReferralStatus);
    }

    [Test]
    public async Task GetReferralById_WhenReferralDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralId = Fixture.Create<Guid>();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferralbyid/{referralId}");

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
