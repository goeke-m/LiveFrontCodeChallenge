namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the response received after updating a referral.
/// </summary>
public class UpdateReferralResponse
{
    /// <summary>
    /// Gets or sets the updated referral information.
    /// </summary>
    public ReferralModel Referral { get; set; } = null!;
}
