namespace CartonCaps.Data.Entities;

/// <summary>
/// Represents the base entity with a unique identifier.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
