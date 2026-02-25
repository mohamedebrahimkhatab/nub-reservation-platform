using Nub.Modules.Catalog.Domain.ValueObjects;
using Nub.Shared.Kernel;

namespace Nub.Modules.Catalog.Domain.Entities;

/// <summary>
/// Represents a type of ticket available for an event with pricing and capacity information.
/// </summary>
public class TicketType : Entity<TicketTypeId>
{
    /// <summary>
    /// Gets the localized name of the ticket type.
    /// </summary>
    public LocalizedString Name { get; private set; }

    /// <summary>
    /// Gets the optional localized description of the ticket type.
    /// </summary>
    public LocalizedString? Description { get; private set; }

    /// <summary>
    /// Gets the price for this ticket type.
    /// </summary>
    public Money Price { get; private set; }

    /// <summary>
    /// Gets the maximum number of tickets available for this type.
    /// </summary>
    public int Capacity { get; private set; }

    #pragma warning disable CS8618
    private TicketType() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TicketType"/> class.
    /// </summary>
    /// <param name="name">The localized name of the ticket type.</param>
    /// <param name="price">The price for this ticket type.</param>
    /// <param name="capacity">The maximum number of tickets available. Must be positive.</param>
    /// <param name="description">Optional localized description of the ticket type.</param>
    /// <exception cref="ArgumentNullException">Thrown when name or price is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when capacity is negative.</exception>
    internal TicketType(TicketTypeId id, LocalizedString name,  Money price, int capacity, LocalizedString? description = null) : base(id)
    {
        SetName(name);
        SetPrice(price);
        SetCapacity(capacity);
        SetDescription(description);
    }
    #pragma warning restore CS8618

    /// <summary>
    /// Updates the ticket type properties with new values.
    /// </summary>
    /// <param name="name">The new localized name.</param>
    /// <param name="price">The new price.</param>
    /// <param name="capacity">The new capacity. Must be positive.</param>
    /// <param name="description">Optional new localized description.</param>
    /// <exception cref="ArgumentNullException">Thrown when name or price is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when capacity is negative.</exception>
    public void Update(LocalizedString name, Money price, int capacity, LocalizedString? description = null)
    {
        SetName(name);
        SetPrice(price);
        SetCapacity(capacity);
        SetDescription(description);
    }

    private void SetName(LocalizedString name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    private void SetPrice(Money price)
    {
        ArgumentNullException.ThrowIfNull(price);
        if (price.Amount < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        Price = price;
    }

    private void SetCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");
        Capacity = capacity;
    }

    private void SetDescription(LocalizedString? description)
    {
        Description = description;
    }

}
