namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the response for updating the status of a referral.
/// </summary>
public class UpdateReferralStatusResponse
{
    /// <summary>
    /// Gets or sets the referral associated with the update.
    /// </summary>
    public ReferralModel Referral { get; set; } = null!;
}
