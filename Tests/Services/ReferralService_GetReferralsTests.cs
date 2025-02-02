using AutoFixture;
using CartonCaps.Data.Entities;
using CartonCaps.Services.Services;
using CartonCaps.Shared.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace CartonCaps.Tests.Services;

[TestFixture]
[Category(TestCategories.Unit)]
public class ReferralService_GetReferralsTests : TestsWithInMemoryDb
{
    private ReferralService _referralService;

    [SetUp]
    public void SetUp()
    {
        var logger = new Mock<ILogger<ReferralService>>();
        _referralService = new ReferralService(ReferralDbContext, logger.Object);
    }

    [Test]
    public async Task GetReferrals_ReturnsEmptyList_WhenNoReferralsExist()
    {
        // Act
        var result = await _referralService.GetReferrals(new Shared.Models.GetReferralsRequest { ReferralCode = "XY7G4D" }, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<GetReferralsResponse>();
        result.Referrals.ShouldBeEmpty();
    }

    [Test]
    public async Task GetReferrals_ReturnsReferrals_WhenReferralsExist()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.Birthday, new DateOnly(1984, 01, 12)).Create();
        var referral = Fixture.Build<Referral>().With(x => x.Referee, referee).Create();
        ReferralDbContext.Referrals.Add(referral);
        await ReferralDbContext.SaveChangesAsync();

        // Act
        var result = await _referralService.GetReferrals(new GetReferralsRequest { ReferralCode = referral.ReferralCode }, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Referrals.ShouldNotBeNull();
        result.Referrals.First().Id.ShouldBe(referral.Id);
        result.Referrals.First().ReferralCode.ShouldBe(referral.ReferralCode);
    }

    [Test]
    public async Task GetReferrals_ReturnsReferrals_WhenReferralsWithSpecificExist()
    {
        // Arrange
        var referee = Fixture.Build<Referee>().With(x => x.Birthday, new DateOnly(1984, 01, 12)).Create();
        var referrals = Fixture.Build<Referral>()
            .With(x => x.Referee, referee)
            .With(x => x.ReferralCode, "XY7G4D")
            .With(x => x.ReferralStatus, ReferralStatus.Pending)
            .CreateMany(5);
        ReferralDbContext.Referrals.AddRange(referrals);
        await ReferralDbContext.SaveChangesAsync();

        // Act
        var result = await _referralService.GetReferrals(new GetReferralsRequest { ReferralCode = "XY7G4D", ReferralStatus = ReferralStatus.Pending }, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Referrals.ShouldNotBeNull();
        result.Referrals.All(x => x.ReferralStatus == ReferralStatus.Pending).ShouldBeTrue();
        result.Referrals.All(x => x.ReferralCode == "XY7G4D").ShouldBeTrue();
    }
}
