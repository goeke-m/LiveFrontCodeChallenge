namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents the status of a referral.
/// </summary>
public enum ReferralStatus
{
    /// <summary>
    /// The referral is pending and has not yet been completed.
    /// </summary>
    Pending,

    /// <summary>
    /// The referral has been completed successfully.
    /// </summary>
    Complete,

    /// <summary>
    /// The referral has expired and is no longer valid.
    /// </summary>
    Expired
}
