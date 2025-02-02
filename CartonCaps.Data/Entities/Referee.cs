using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartonCaps.Data.Entities;

/// <summary>
/// Represents a referee entity with personal details and optional referral information.
/// </summary>
public class Referee : BaseEntity
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
    public virtual Referral? Referral { get; set; } = null;
}

public class RefereeConfiguration : IEntityTypeConfiguration<Referee>
{
    void IEntityTypeConfiguration<Referee>.Configure(EntityTypeBuilder<Referee> builder)
    {
        builder.ToTable(nameof(Referee));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Birthday).IsRequired();
        builder.Property(x => x.Zipcode).IsRequired();
    }
}
