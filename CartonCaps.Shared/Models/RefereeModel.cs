namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a referee with personal details and an optional referral.
/// </summary>
public class RefereeModel
{
    /// <summary>
    /// Gets or sets the first name of the referee.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the referee.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the referee.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the referee.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the referral associated with the referee.
    /// </summary>
    public ReferralModel? Referral { get; set; } = null;
}
