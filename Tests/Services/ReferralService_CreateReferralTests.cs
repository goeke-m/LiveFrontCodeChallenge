using AutoFixture;
using CartonCaps.Services.Services;
using CartonCaps.Shared.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace CartonCaps.Tests.Services
{
    [TestFixture]
    [Category(TestCategories.Unit)]
    public class ReferralService_CreateReferralTests : TestsWithInMemoryDb
    {
        private ReferralService _referralService;

        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<ReferralService>>();
            _referralService = new ReferralService(ReferralDbContext, logger.Object);
        }

        [Test]
        public async Task CreateReferral_ValidRequest_ReturnsCreateReferralResponse()
        {
            // Arrange
            var request = Fixture.Build<CreateReferralRequest>().With(x => x.Birthday, new DateOnly(1980, 4, 23)).Create();

            // Act
            var response = await _referralService.CreateReferral(request, CancellationToken.None);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBeOfType<CreateReferralResponse>();
            response.Referral.ShouldNotBeNull();
            response.Referral.Id.ShouldNotBe(Guid.Empty);
        }


    }
}
