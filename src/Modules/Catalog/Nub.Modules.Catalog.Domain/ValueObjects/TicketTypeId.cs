namespace Nub.Modules.Catalog.Domain.ValueObjects;

/// <summary>
/// Strongly-typed identifier for TicketType.
/// Prevents primitive obsession and provides type safety.
/// </summary>
public record TicketTypeId
{
    public Guid Value { get; }

    private TicketTypeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("TicketTypeId cannot be empty.", nameof(value));
        
        Value = value;
    }

    /// <summary>
    /// Creates a new TicketTypeId with a generated Version 7 GUID.
    /// </summary>
    public static TicketTypeId Create() => new(Guid.CreateVersion7());

    /// <summary>
    /// Creates a TicketTypeId from an existing GUID value.
    /// </summary>
    public static TicketTypeId From(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}