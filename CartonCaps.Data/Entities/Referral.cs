using CartonCaps.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartonCaps.Data.Entities;

/// <summary>
/// Represents a referral entity with details about the referee and referral status.
/// </summary>
public class Referral : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the referee.
    /// </summary>
    public Guid RefereeId { get; set; }

    /// <summary>
    /// Gets or sets the referee associated with the referral.
    /// </summary>
    public virtual Referee Referee { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the referral.
    /// </summary>
    public ReferralStatus ReferralStatus { get; set; } = ReferralStatus.Pending;

    /// <summary>
    /// Gets or sets the referral code.
    /// </summary>
    public required string ReferralCode { get; set; }
}

public class ReferralConfiguration : IEntityTypeConfiguration<Referral>
{
    void IEntityTypeConfiguration<Referral>.Configure(EntityTypeBuilder<Referral> builder)
    {
        builder.ToTable(nameof(Referral));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReferralCode).IsRequired();
        builder.Property(x => x.ReferralStatus).IsRequired();
        builder.HasOne(x => x.Referee);
    }
}
