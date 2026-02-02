# ADR-001: Modular Monolith Architecture

**Date:** 2026-01-31  
**Status:** Accepted  
**Deciders:** Technical Lead  

## Context

We need to choose an architectural pattern for the Nub event reservation platform. The system will have multiple bounded contexts (Catalog, Bookings, Payments, Users) that need clear separation but must work together transactionally for critical operations like ticket booking.

Key considerations:
- Solo developer initially (limited operational capacity)
- Need for transactional consistency (prevent double-booking)
- Future potential to scale specific components independently
- Learning and demonstrating modern architecture patterns
- Cost-effectiveness for MVP and early stages

## Decision

We will implement a **Modular Monolith** architecture with the following characteristics:

1. **Logical Modules** - Separate projects for each bounded context
2. **Clean Architecture** - Each module follows 4-layer architecture
3. **Defined Boundaries** - Modules communicate through interfaces and events
4. **Single Deployment** - All modules deployed together as one application
5. **Shared Database** - Logical schema separation, not physical

## Consequences

### Positive Consequences

- **Simpler Operations** - Single deployment reduces DevOps complexity
- **Faster Development** - No distributed system overhead (network calls, eventual consistency)
- **ACID Transactions** - Easy to maintain data consistency across modules
- **Lower Cost** - Single server/container vs. orchestrating multiple services
- **Easier Debugging** - All code in one process, better stack traces
- **Evolution Path** - Modules can be extracted to microservices when needed

### Negative Consequences

- **Coupled Deployment** - All modules must deploy together
- **Scaling Limitations** - Can't scale individual modules independently
- **Technology Lock-in** - All modules must use same stack (.NET)
- **Single Point of Failure** - If application crashes, everything is down

### Risks

- **Boundary Erosion** - Developers might take shortcuts and break module boundaries  
  *Mitigation:* Architecture tests, code reviews, clear conventions

- **Growing Complexity** - As modules grow, single codebase could become unwieldy  
  *Mitigation:* Monitor module size, extract when truly needed

## Alternatives Considered

### Alternative 1: Traditional Layered Monolith

**Pros:**
- Simpler structure (fewer projects)
- Faster initial development
- Well-understood pattern

**Cons:**
- Poor separation of concerns
- Difficult to extract features later
- Code becomes tightly coupled
- Doesn't demonstrate modern patterns

**Rejected because:** Doesn't provide learning value or future flexibility

### Alternative 2: Microservices Architecture

**Pros:**
- Independent deployment and scaling
- Technology diversity possible
- Clear service boundaries
- Industry trendy

**Cons:**
- Significant operational complexity (service discovery, API gateway, distributed tracing)
- Eventual consistency challenges (harder to prevent double-booking)
- Network latency between services
- More expensive infrastructure
- Overkill for MVP and solo developer

**Rejected because:** Operational burden outweighs benefits at current scale

### Alternative 3: Vertical Slice Architecture

**Pros:**
- Feature-focused organization
- Very popular recently
- Simpler than modules

**Cons:**
- Harder to enforce bounded context boundaries
- Doesn't prepare well for future service extraction
- Less clear domain separation

**Rejected because:** Want to learn DDD and prepare for potential microservices evolution

## References

- [Modular Monolith: A Primer](https://www.kamilgrzybek.com/design/modular-monolith-primer/)
- [Microservices vs Monolith](https://martinfowler.com/articles/microservices.html)
- [Shopify's Modular Monolith](https://shopify.engineering/deconstructing-monolith-designing-software-maximizes-developer-effectiveness)

---

*This decision can be revisited when:*
- Traffic exceeds 10,000 requests/minute
- Module deployment cadences significantly diverge
- Operational team grows beyond 5 developers
- Specific module has vastly different scaling requirements