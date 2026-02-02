# Architecture Overview

## Introduction

Nub is built as a **Modular Monolith** using **Clean Architecture** principles and **Domain-Driven Design** patterns. This architecture provides clear boundaries between modules while maintaining the simplicity of a single deployment unit.

## Why Modular Monolith?

### Advantages
- **Simpler deployment** - Single application, one database
- **Easier development** - Shared transactions, no distributed complexity
- **Clear boundaries** - Modules enforce separation of concerns
- **Cost-effective** - Lower infrastructure costs than microservices
- **Evolution path** - Modules can be extracted to services when needed

### When to Extract a Module
- Module has different scaling requirements
- Module needs independent deployment cadence
- Team grows and needs parallel development
- Technology stack differs significantly

---

## High-Level Architecture

```
┌─────────────────────────────────────────────────────────┐
│                      API Layer                          │
│              (ASP.NET Core Web API)                     │
└──────────────┬──────────────┬──────────────┬───────────┘
               │              │              │
       ┌───────▼──────┐ ┌────▼──────┐ ┌────▼──────┐
       │   Catalog    │ │ Bookings  │ │  Payments │
       │   Module     │ │  Module   │ │  Module   │
       └──────┬───────┘ └─────┬─────┘ └─────┬─────┘
              │               │              │
              └───────┬───────┴──────┬───────┘
                      │              │
              ┌───────▼──────────────▼───────┐
              │   Shared Kernel / Events     │
              └──────────────────────────────┘
                      │
              ┌───────▼──────────┐
              │   PostgreSQL     │
              └──────────────────┘
```

---

## Module Structure

Each module follows **Clean Architecture** with four layers:

### 1. Domain Layer (Core)
**Dependencies:** None  
**Contains:**
- Entities and Aggregates
- Value Objects
- Domain Events
- Business Rules
- Domain Exceptions

**Example:**
```
Catalog.Domain/
├── Events/
│   ├── Event.cs (Aggregate Root)
│   ├── TicketType.cs (Entity)
│   └── Money.cs (Value Object)
├── DomainEvents/
│   └── EventPublishedDomainEvent.cs
└── Exceptions/
    └── InsufficientCapacityException.cs
```

### 2. Application Layer
**Dependencies:** Domain  
**Contains:**
- Use Cases (Commands & Queries)
- Command/Query Handlers
- DTOs
- Interfaces (Repositories, Services)
- Validation Rules

**Example:**
```
Catalog.Application/
├── Commands/
│   ├── CreateEvent/
│   │   ├── CreateEventCommand.cs
│   │   ├── CreateEventCommandHandler.cs
│   │   └── CreateEventCommandValidator.cs
├── Queries/
│   ├── GetEvents/
│   │   ├── GetEventsQuery.cs
│   │   └── GetEventsQueryHandler.cs
└── Interfaces/
    └── IEventRepository.cs
```

### 3. Infrastructure Layer
**Dependencies:** Application, Domain  
**Contains:**
- EF Core DbContext
- Repository Implementations
- External Service Integrations
- Data Migrations

**Example:**
```
Catalog.Infrastructure/
├── Persistence/
│   ├── CatalogDbContext.cs
│   ├── Configurations/
│   │   └── EventConfiguration.cs
│   └── Repositories/
│       └── EventRepository.cs
└── Migrations/
```

### 4. API Layer (Presentation)
**Dependencies:** Application  
**Contains:**
- Controllers/Endpoints
- Request/Response Models
- Filters and Middleware
- API Documentation

**Example:**
```
Catalog.Api/
├── Controllers/
│   └── EventsController.cs
├── Models/
│   ├── CreateEventRequest.cs
│   └── EventResponse.cs
└── DependencyInjection.cs
```

---

## Module Communication

Modules communicate through **well-defined boundaries**:

### 1. Domain Events (Eventual Consistency)
**When:** State changes that other modules care about  
**How:** Publish-Subscribe pattern

```csharp
// Catalog Module publishes
DomainEvents.Raise(new EventPublishedDomainEvent(eventId));

// Bookings Module subscribes
public class EventPublishedDomainEventHandler 
    : IDomainEventHandler<EventPublishedDomainEvent>
{
    // React to event being published
}
```

### 2. Application Service Interfaces (Synchronous)
**When:** Need immediate response from another module  
**How:** Dependency Inversion (depend on abstraction)

```csharp
// In Bookings.Application
public interface ICatalogService
{
    Task<bool> HasAvailableCapacity(EventId eventId, int quantity);
}

// Implemented in Catalog.Infrastructure
// Registered in DI container
```

### 3. Shared Kernel
**When:** Truly shared concepts across all modules  
**What:** Common value objects, base classes, utilities

```csharp
// Shared.Kernel
public abstract class Entity<TId>
public abstract class ValueObject
public interface IDomainEvent
```

---

## Design Patterns Used

### Domain-Driven Design (DDD)
- **Aggregates** - Consistency boundaries (Event, Booking)
- **Entities** - Objects with identity (TicketType)
- **Value Objects** - Immutable concepts (Money, DateRange)
- **Domain Events** - Significant state changes
- **Repositories** - Aggregate persistence abstraction

### CQRS (Command Query Responsibility Segregation)
- **Commands** - Mutate state (CreateEvent, BookTickets)
- **Queries** - Read data (GetEvents, GetBooking)
- **Separate Models** - Optimized for their purpose

### Other Patterns
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Mediator Pattern** - MediatR for request handling
- **Outbox Pattern** - Reliable event publishing (future)
- **Saga Pattern** - Distributed transactions (Payments)

---

## Database Strategy

### Single Database, Logical Separation
- Each module owns its tables (schemas)
- No cross-module foreign keys
- Module boundaries enforced by code, not DB

**Schema Organization:**
```
nubdb
├── catalog schema
│   ├── events
│   ├── ticket_types
│   └── event_categories
├── bookings schema
│   ├── bookings
│   ├── booking_items
│   └── reservations
└── payments schema
    ├── payments
    └── refunds
```

### Why Not Separate Databases?
- **Current Phase:** Simpler, faster development
- **Transactions:** Easy cross-module transactions when needed
- **Future:** Can extract to separate DBs when scaling requires it

---

## Key Architectural Decisions

See [Architecture Decision Records](./decisions/) for detailed rationale on:
- [ADR-001: Modular Monolith over Microservices](./decisions/001-modular-monolith.md)
- ADR-002: PostgreSQL over SQL Server (coming soon)
- ADR-003: CQRS with MediatR (coming soon)
- ADR-004: Domain Events for Module Communication (coming soon)

---

## Technology Decisions

| Concern | Technology | Rationale |
|---------|-----------|-----------|
| Framework | .NET 10 (LTS) | Latest stable, long-term support |
| Database | PostgreSQL 16 | Open source, powerful, JSON support |
| ORM | Entity Framework Core | Best .NET ORM, code-first migrations |
| CQRS | MediatR | Simple, popular, well-documented |
| Validation | FluentValidation | Expressive, testable |
| Logging | Serilog | Structured logging, many sinks |
| Containers | Docker | Industry standard, easy local dev |

---

## Quality Attributes

### Performance
- **Caching** - Redis for frequently accessed data
- **Indexing** - Strategic database indexes
- **Async/Await** - Non-blocking I/O throughout

### Reliability
- **Idempotency** - Safe retries for critical operations
- **Concurrency Control** - Optimistic locking for bookings
- **Health Checks** - Database, dependencies monitoring

### Security
- **Authentication** - JWT tokens
- **Authorization** - Role/claim-based policies
- **Input Validation** - All endpoints validated
- **SQL Injection Prevention** - Parameterized queries (EF Core)

### Maintainability
- **Clean Architecture** - Clear dependencies
- **SOLID Principles** - Throughout codebase
- **Testing** - Unit, integration, architecture tests
- **Documentation** - ADRs, README files, XML comments

---

## Future Considerations

### Potential Enhancements
- **Event Sourcing** - For booking history and audit trail
- **Read Models** - Separate optimized read database (CQRS full pattern)
- **Message Broker** - RabbitMQ/Azure Service Bus for async events
- **API Gateway** - When modules become services
- **GraphQL** - Alternative to REST for complex queries

### Extraction Candidates
If we need to scale independently:
1. **Payments Module** - Separate service (PCI compliance isolation)
2. **Notifications Module** - Separate service (high volume, different scaling)
3. **Analytics Module** - Separate service (read-heavy, different DB)

---

*Last Updated: January 31, 2026*