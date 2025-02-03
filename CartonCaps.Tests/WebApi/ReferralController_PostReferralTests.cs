using System.Net;
using System.Net.Http.Json;

namespace CartonCaps.Tests.WebApi;

public class ReferralController_PostReferralTests : TestsWithTestContainer
{
    [Test]
    public void PostReferral_WhenFirstNameIsNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = string.Empty,
            LastName = "Doe",
            PhoneNumber = "555-902-6489",
            Email = "12345",
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
            LastName = string.Empty,
            PhoneNumber = "555-902-6489",
            Email = "12345",
            ReferralCode = "ABC123",
            ReferralStatus = ReferralStatus.Pending
        };

        // Act
        var response = client.PostAsJsonAsync("api/v1/referral/createreferral", request).Result;

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public void PostReferral_WhenPhoneNumberAndEmailAreNotValid_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new CreateReferralRequest
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = null,
            Email = "",
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
            PhoneNumber = "555-902-6489",
            Email = "12345",
            ReferralCode = string.Empty,
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
            PhoneNumber = "555-902-6489",
            Email = "12345",
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
            PhoneNumber = "555-902-6489",
            Email = "12345",
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
        referral.Referral.Referee.PhoneNumber.ShouldBe(request.PhoneNumber);
        referral.Referral.Referee.Email.ShouldBe(request.Email);
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
            PhoneNumber = "555-902-6489",
            Email = "12345",
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
            PhoneNumber = "555-902-6489",
            Email = "12345",
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
