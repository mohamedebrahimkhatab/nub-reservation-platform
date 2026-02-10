namespace Nub.Modules.Catalog.Domain.ValueObjects;

/// <summary>
/// Strongly-typed identifier for Event aggregate.
/// Prevents primitive obsession and provides type safety.
/// </summary>
public record EventId
{
    public Guid Value { get; }

    private EventId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("EventId cannot be empty.", nameof(value));
        
        Value = value;
    }

    /// <summary>
    /// Creates a new EventId with a generated Version 7 GUID.
    /// </summary>
    public static EventId Create() => new(Guid.CreateVersion7());

    /// <summary>
    /// Creates an EventId from an existing GUID value.
    /// </summary>
    public static EventId From(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}