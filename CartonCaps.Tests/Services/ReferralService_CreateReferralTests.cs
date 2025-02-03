using CartonCaps.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

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
            var request = Fixture.Build<CreateReferralRequest>().With(x => x.PhoneNumber, "555-902-6489").Create();

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
