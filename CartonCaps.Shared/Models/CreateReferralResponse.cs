namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the response received after creating a referral.
/// </summary>
public class CreateReferralResponse
{
    /// <summary>
    /// Gets or sets the referral details.
    /// </summary>
    public ReferralModel Referral { get; set; } = null!;
}
