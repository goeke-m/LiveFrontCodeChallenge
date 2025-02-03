using CartonCaps.Data.Entities;
using CartonCaps.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartonCaps.Tests.Services;

[TestFixture]
[Category(TestCategories.Unit)]
public class ReferralService_UpdateReferralStatusTests : TestsWithInMemoryDb
{
    private ReferralService _referralService;

    [SetUp]
    public void SetUp()
    {
        var logger = new Mock<ILogger<ReferralService>>();
        _referralService = new ReferralService(ReferralDbContext, logger.Object);
    }

    [Test]
    public async Task UpdateReferralStatus_ShouldUpdateStatus()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.PhoneNumber, "555-902-6489").Create();
        var referral = Fixture.Build<Referral>().With(x => x.Referee, referee).With(x => x.ReferralStatus, ReferralStatus.Pending).Create();

        ReferralDbContext.Referrals.Add(referral);
        await ReferralDbContext.SaveChangesAsync();

        var request = new UpdateReferralStatusRequest
        {
            ReferralId = referral.Id,
            ReferralStatus = ReferralStatus.Complete
        };

        // Act
        await _referralService.UpdateReferralStatus(request, CancellationToken.None);

        // Assert
        var updatedReferral = ReferralDbContext.Referrals.FirstOrDefault(x => x.Id == referral.Id);
        updatedReferral?.ReferralStatus.ShouldBe(request.ReferralStatus);
    }

    [Test]
    public async Task UpdateReferralStatus_ShouldReturnUpdatedReferral()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.PhoneNumber, "555-902-6489").Create();
        var referral = Fixture.Build<Referral>().With(x => x.Referee, referee).With(x => x.ReferralStatus, ReferralStatus.Pending).Create();

        ReferralDbContext.Referrals.Add(referral);
        await ReferralDbContext.SaveChangesAsync();

        var request = new UpdateReferralStatusRequest
        {
            ReferralId = referral.Id,
            ReferralStatus = ReferralStatus.Complete
        };

        // Act
        var result = await _referralService.UpdateReferralStatus(request, CancellationToken.None);

        // Assert
        result.Referral.ReferralStatus.ShouldBe(request.ReferralStatus);
    }

    [Test]
    public async Task UpdateReferralStatus_WhenReferralDoesNotExist_ReturnEmptyResponse()
    {
        // Arrange
        var request = new UpdateReferralStatusRequest
        {
            ReferralId = Guid.NewGuid(),
            ReferralStatus = ReferralStatus.Complete
        };

        // Act
        var result = await _referralService.UpdateReferralStatus(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<UpdateReferralStatusResponse>();
        result.Referral.ShouldBeNull();
    }
}
