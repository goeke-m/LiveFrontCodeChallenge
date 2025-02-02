namespace CartonCaps.Shared.Models;

/// <summary>
/// Represents a request to get referrals.
/// </summary>
public class GetReferralsRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetReferralsRequest"/> class.
    /// </summary>
    public GetReferralsRequest() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetReferralsRequest"/> class with the specified referral code.
    /// </summary>
    /// <param name="referralCode">The referral code.</param>
    public GetReferralsRequest(string referralCode) : this(referralCode, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetReferralsRequest"/> class with the specified referral code and referral status.
    /// </summary>
    /// <param name="referralCode">The referral code.</param>
    /// <param name="referralStatus">The referral status.</param>
    public GetReferralsRequest(string referralCode, ReferralStatus? referralStatus)
    {
        ReferralCode = referralCode;
        ReferralStatus = referralStatus;
    }

    /// <summary>
    /// Gets or sets the referral code.
    /// </summary>
    public required string ReferralCode { get; set; }

    /// <summary>
    /// Gets or sets the referral status.
    /// </summary>
    public ReferralStatus? ReferralStatus { get; set; }
}
