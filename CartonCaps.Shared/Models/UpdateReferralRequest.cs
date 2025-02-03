namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a request to update a referral.
/// </summary>
public class UpdateReferralRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the referral.
    /// </summary>
    public Guid ReferralId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the person being referred.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the person being referred.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the person being referred.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the person being referred.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the status of the referral.
    /// </summary>
    public ReferralStatus ReferralStatus { get; set; } = ReferralStatus.Pending;

    /// <summary>
    /// Gets or sets the referral code.
    /// </summary>
    public required string ReferralCode { get; set; }
}
