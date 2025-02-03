using System.Net;

namespace CartonCaps.Tests.WebApi;

public class ReferralController_GetReferralsTests : TestsWithTestContainer
{
    [Test]
    public async Task GetReferrals_WhenReferralsWithReferralCodeExist_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferrals?referralCode={referral.ReferralCode}");

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task GetReferrals_WhenReferralsWithReferralCodeExist_ReturnsReferrals()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferrals?referralCode={referral.ReferralCode}");
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetReferralsResponse>(httpResponseString);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        response?.Referrals.Count().ShouldBe(1);
        response?.Referrals.First().ReferralCode.ShouldBe(referral.ReferralCode);
    }

    [Test]
    public async Task GetReferrals_WhenReferralsWithReferralCodeAndReferralStatusExist_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var pendingReferrals = SeedReferrals(3, referralCode);
        // Set 2 to complete for a controlled experiment
        var completeReferrals = SeedReferrals(2, referralCode, ReferralStatus.Complete);

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferrals?referralCode={referralCode}&referralStatus=1");

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task GetReferrals_WhenReferralsWithReferralCodeAndReferralStatusExist_ReturnsReferrals()
    {
        // Arrange
        var client = Factory.CreateClient();

        var referralCode = Fixture.Create<string>();
        var pendingReferrals = SeedReferrals(3, referralCode);

        // Set 2 to complete for a controlled experiment
        var completeReferrals = SeedReferrals(2, referralCode, ReferralStatus.Complete);

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferrals?referralCode={referralCode}&referralStatus=1");
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetReferralsResponse>(httpResponseString);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Referrals.Count().ShouldBe(2);
        response.Referrals.All(x => x.ReferralCode == referralCode).ShouldBeTrue();
        response.Referrals.All(x => x.ReferralStatus == ReferralStatus.Complete).ShouldBeTrue();
    }

    [Test]
    public async Task GetReferrals_WhenReferralsWithReferralCodeDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralCode = Fixture.Create<string>();

        // Act
        var httpResponse = await client.GetAsync($"api/v1/referral/getreferrals?referralCode={referralCode}");

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
