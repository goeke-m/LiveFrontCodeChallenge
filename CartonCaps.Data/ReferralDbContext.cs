using CartonCaps.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartonCaps.Data;

/// <summary>
/// Represents the database context for referrals and referees.
/// </summary>
public class ReferralDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReferralDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public ReferralDbContext(DbContextOptions<ReferralDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="Referral"/> entities.
    /// </summary>
    public DbSet<Referral> Referrals { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="Referee"/> entities.
    /// </summary>
    public DbSet<Referee> Referees { get; set; }
}
