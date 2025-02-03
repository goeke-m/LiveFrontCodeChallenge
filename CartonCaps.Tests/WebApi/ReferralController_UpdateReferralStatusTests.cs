using System.Net;
using System.Text;

namespace CartonCaps.Tests.WebApi;

public class ReferralController_UpdateReferralStatusTests : TestsWithTestContainer
{
    [Test]
    public async Task UpdateReferralStatus_WhenReferralExists_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        var jsonRequest = JsonConvert.SerializeObject(new UpdateReferralStatusRequest { ReferralId = referral.Id, ReferralStatus = ReferralStatus.Complete });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PatchAsync("api/v1/referral/updatereferralstatus", content);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
    }

    [Test]
    public async Task UpdateReferralStatus_WhenReferralExists_ReturnsUpdatedReferral()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        var jsonRequest = JsonConvert.SerializeObject(new UpdateReferralStatusRequest { ReferralId = referral.Id, ReferralStatus = ReferralStatus.Complete });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PatchAsync("api/v1/referral/updatereferralstatus", content);
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<UpdateReferralStatusResponse>(httpResponseString);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        response.Referral.ReferralStatus.ShouldBe(ReferralStatus.Complete);
    }

    [Test]
    public async Task UpdateReferralStatus_WhenReferralDoesNotExists_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralId = Fixture.Create<Guid>();

        var jsonRequest = JsonConvert.SerializeObject(new UpdateReferralStatusRequest { ReferralId = referralId, ReferralStatus = ReferralStatus.Complete });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PatchAsync("api/v1/referral/updatereferralstatus", content);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

}
