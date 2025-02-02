using System.Net;
using System.Net.Http.Json;

namespace CartonCaps.Tests.WebApi;

[TestFixture]
[Category(TestCategories.Integration)]
public class ReferralController_PostReferralTests : TestsWithTestContainer
{
    [Test]
    public void PostReferral_WhenFirstNameIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };

        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenLastNameIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenBirthdayIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = null,
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenZipcodeIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenReferralCodeIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenRequestIsValid_ReturnsCreated()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Test]
    public void PostReferral_WhenRequestIsValid_ReturnsReferral()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        var referral = response.Content.ReadFromJsonAsync<CreateReferralResponse>().Result;
        
        // Assert
        referral.ShouldNotBeNull();
        referral.Referral.Referee.FirstName.ShouldBe(request.FirstName);
        referral.Referral.Referee.LastName.ShouldBe(request.LastName);
        referral.Referral.Referee.Birthday.ShouldBe(request.Birthday);
        referral.Referral.Referee.Zipcode.ShouldBe(request.Zipcode);
        referral.Referral.ReferralCode.ShouldBe(request.ReferralCode);
        referral.Referral.ReferralStatus.ShouldBe(request.ReferralStatus);
    }

    [Test]
    public void PostReferral_WhenRequestIsValid_ReturnsLocationHeader()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        
        // Assert
        response.Headers.Location.ShouldNotBeNull();
    }

    [Test]
    public void PostReferral_WhenRequestIsValid_ReturnsLocationHeaderWithReferralId()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        var referral = response.Content.ReadFromJsonAsync<CreateReferralResponse>().Result;
        
        // Assert
        response.Headers.Location.ToString().ShouldContain(referral.Referral.Id.ToString());
    }

    [Test]
    public void PostReferral_WhenRequestIsValid_ReturnsLocationHeaderWithReferralIdInPath()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1980, 1, 1),
            Zipcode = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };
        
        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;
        var referral = response.Content.ReadFromJsonAsync<CreateReferralResponse>().Result;
        
        // Assert
        response.Headers.Location.ToString().ShouldContain(referral.Referral.Id.ToString());
    }
}
