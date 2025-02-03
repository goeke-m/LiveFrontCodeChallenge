using CartonCaps.Data.Entities;
using CartonCaps.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartonCaps.Tests.Services;

[TestFixture]
[Category(TestCategories.Unit)]
public class ReferralService_GetReferralTests : TestsWithInMemoryDb
{
    private ReferralService _referralService;

    [SetUp]
    public void SetUp()
    {
        var logger = new Mock<ILogger<ReferralService>>();
        _referralService = new ReferralService(ReferralDbContext, logger.Object);
    }

    [Test]
    public async Task GetReferral_WhenReferralExists_ReturnsReferral()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.PhoneNumber, "555-902-6489").Create();
        var referral = Fixture.Build<Referral>().With(x => x.Referee, referee).Create();
        ReferralDbContext.Referrals.Add(referral);
        await ReferralDbContext.SaveChangesAsync();

        // Act
        var result = await _referralService.GetReferralById(referral.Id, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Referral.Id.ShouldBe(referral.Id);
        result.Referral.ReferralCode.ShouldBe(referral.ReferralCode);
        result.Referral.ReferralStatus.ShouldBe(referral.ReferralStatus);
        result.Referral.Referee.FirstName.ShouldBe(referral.Referee.FirstName);
        result.Referral.Referee.LastName.ShouldBe(referral.Referee.LastName);
        result.Referral.Referee.PhoneNumber.ShouldBe(referral.Referee.PhoneNumber);
        result.Referral.Referee.Email.ShouldBe(referral.Referee.Email);
    }

    [Test]
    public async Task GetReferral_WhenReferralDoesNotExist_ReturnsEmptyResponse()
    {
        // Act
        var result = await _referralService.GetReferralById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<GetReferralByIdResponse>();
        result.Referral.ShouldBeNull();
    }
}
