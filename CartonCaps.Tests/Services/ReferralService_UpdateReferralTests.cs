using CartonCaps.Data.Entities;
using CartonCaps.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartonCaps.Tests.Services;

[TestFixture]
[Category(TestCategories.Unit)]
public class ReferralService_UpdateReferralTests : TestsWithInMemoryDb
{
    private ReferralService _referralService;

    [SetUp]
    public void SetUp()
    {
        var logger = new Mock<ILogger<ReferralService>>();
        _referralService = new ReferralService(ReferralDbContext, logger.Object);
    }

    [Test]
    public async Task UpdateReferral_ValidReferral_UpdatesSuccessfully()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.PhoneNumber, "555-902-6489").Create();
        var referral = Fixture.Build<Referral>().With(x => x.Referee, referee).With(x => x.ReferralStatus, ReferralStatus.Pending).Create();

        ReferralDbContext.Referrals.Add(referral);
        await ReferralDbContext.SaveChangesAsync();

        // Act
        await _referralService.UpdateReferral(new UpdateReferralRequest
        {
            ReferralId = referral.Id,
            FirstName = "Updated",
            LastName = "Referral",
            ReferralCode = referral.ReferralCode
        }, default);

        // Assert
        var updatedReferral = await ReferralDbContext.Referrals.FindAsync(referral.Id);
        updatedReferral.ShouldNotBeNull();
        updatedReferral.Referee.FirstName.ShouldBe("Updated");
        updatedReferral.Referee.LastName.ShouldBe("Referral");
        updatedReferral.ReferralCode.ShouldBe(referral.ReferralCode);
        updatedReferral.ReferralStatus.ShouldBe(referral.ReferralStatus);
    }

    [Test]
    public async Task UpdateReferral_WhenReferralDoesNotExist_ReturnsEmptyResponse()
    {
        var result = await _referralService.UpdateReferral(new UpdateReferralRequest
        {
            ReferralId = Guid.NewGuid(),
            FirstName = "Updated",
            LastName = "Referral",
            ReferralCode = "DoesNotExist"
        }, default);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<UpdateReferralResponse>();
        result.Referral.ShouldBeNull();
    }
}
