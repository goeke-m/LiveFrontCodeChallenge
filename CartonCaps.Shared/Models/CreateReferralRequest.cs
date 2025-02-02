using FluentValidation;

namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a request to create a referral.
/// </summary>
public class CreateReferralRequest
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
    /// Gets or sets the status of the referral.
    /// </summary>
    public ReferralStatus ReferralStatus { get; set; } = ReferralStatus.Pending;

    /// <summary>
    /// Gets or sets the referral code.
    /// </summary>
    public required string ReferralCode { get; set; }
}

/// <summary>
/// Validator for the <see cref="CreateReferralRequest"/> class.
/// </summary>
public class CreateReferralRequestValidator : AbstractValidator<CreateReferralRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReferralRequestValidator"/> class.
    /// </summary>
    public CreateReferralRequestValidator()
    {
        RuleFor(x => x.ReferralCode).NotEmpty().WithMessage("Referral code must be included when creating a referral.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("The Referee's first name must be included when creating a referral.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("The Referee's last name must be included when creating a referral.");
        RuleFor(x => x.Birthday).NotEmpty().WithMessage("The Referee's birthday must be included when creating a referral.");
        RuleFor(x => x.Zipcode).NotEmpty().WithMessage("The Referee's zipcode must be included when creating a referral.");
    }
}
