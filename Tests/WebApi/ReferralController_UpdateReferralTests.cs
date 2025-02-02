﻿using System.Net;
using System.Text;
using CartonCaps.Data.Entities;

namespace CartonCaps.Tests.WebApi;

[TestFixture]
[Category(TestCategories.Integration)]
public class ReferralController_UpdateReferralTests : TestsWithTestContainer
{
    [Test]
    public async Task UpdateReferral_WhenReferralExists_ReturnsOk()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();
        var updatedReferral = Fixture.Build<UpdateReferralRequest>().With(x => x.ReferralId, referral.Id)
            .With(x => x.Birthday, referral.Referee.Birthday)
            .With(x => x.FirstName, "John").Create();

        var jsonRequest = JsonConvert.SerializeObject(updatedReferral);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PutAsync("api/v1/referral/updatereferral", content);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
    }

    [Test]
    public async Task UpdateReferral_WhenReferralExists_ReturnsUpdatedReferral()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralCode = Fixture.Create<string>();
        var referral = SeedReferrals(1, referralCode).First();
        var updatedReferral = Fixture.Build<UpdateReferralRequest>().With(x => x.ReferralId, referral.Id)
            .With(x => x.Birthday, referral.Referee.Birthday)
            .With(x => x.FirstName, "John").Create();

        var jsonRequest = JsonConvert.SerializeObject(updatedReferral);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PutAsync("api/v1/referral/updatereferral", content);
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<UpdateReferralResponse>(httpResponseString);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        response.Referral.Referee.FirstName.ShouldBe("John");
    }

    [Test]
    public async Task UpdateReferral_WhenReferralDoesNotExists_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        var referralId = Fixture.Create<Guid>();
        var updatedReferral = Fixture.Build<UpdateReferralRequest>().With(x => x.ReferralId, referralId)
            .With(x => x.Birthday, new DateOnly(1980,1,14))
            .With(x => x.FirstName, "John").Create();

        var jsonRequest = JsonConvert.SerializeObject(updatedReferral);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await client.PutAsync("api/v1/referral/updatereferral", content);

        // Assert
        httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

}
