namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a request to update the status of a referral.
/// </summary>
public class UpdateReferralStatusRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the referral.
    /// </summary>
    public Guid ReferralId { get; set; }

    /// <summary>
    /// Gets or sets the status of the referral.
    /// </summary>
    public ReferralStatus ReferralStatus { get; set; }
}
