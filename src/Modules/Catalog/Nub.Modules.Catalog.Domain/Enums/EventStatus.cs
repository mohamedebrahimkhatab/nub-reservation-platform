namespace Nub.Modules.Catalog.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of an Event.
/// 
/// Valid transitions:
/// - Draft → Published → Completed
/// - Draft → Cancelled
/// - Published → Cancelled
/// - Published → Draft (unpublish if no tickets sold)
/// </summary>
public enum EventStatus
{
    /// <summary>
    /// Event is being created and is not visible to the public.
    /// Can be edited freely.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Event is published and visible to the public.
    /// Registration may be open depending on registration window.
    /// Can be unpublished if no tickets have been sold.
    /// </summary>
    Published = 1,

    /// <summary>
    /// Event has been cancelled by the organizer.
    /// No further bookings allowed.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Event has completed (EndDate has passed).
    /// Remains in system for historical and reporting purposes.
    /// </summary>
    Completed = 3
}