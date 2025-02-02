namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a referral in the system.
/// </summary>
public class ReferralModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the referral.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the referee.
    /// </summary>
    public Guid RefereeId { get; set; }

    /// <summary>
    /// Gets or sets the referee associated with the referral.
    /// </summary>
    public RefereeModel Referee { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the referral.
    /// </summary>
    public ReferralStatus ReferralStatus { get; set; } = ReferralStatus.Pending;

    /// <summary>
    /// Gets or sets the referral code.
    /// </summary>
    public required string ReferralCode { get; set; }
}
