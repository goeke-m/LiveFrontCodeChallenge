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
    /// Gets or sets the birthday of the referee.
    /// </summary>
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// Gets or sets the zipcode of the referee.
    /// </summary>
    public string? Zipcode { get; set; }

    /// <summary>
    /// Gets or sets the referral associated with the referee.
    /// </summary>
    public ReferralModel? Referral { get; set; } = null;
}
