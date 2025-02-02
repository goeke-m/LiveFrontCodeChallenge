namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the response for getting a referral by its ID.
/// </summary>
public class GetReferralByIdResponse
{
    /// <summary>
    /// Gets or sets the referral details.
    /// </summary>
    public ReferralModel Referral { get; set; } = null!;
}
