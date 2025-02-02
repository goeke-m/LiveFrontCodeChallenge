namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the response containing a list of referrals.
/// </summary>
public class GetReferralsResponse
{
    /// <summary>
    /// Gets or sets the collection of referrals.
    /// </summary>
    public IEnumerable<ReferralModel> Referrals { get; set; } = [];
}
