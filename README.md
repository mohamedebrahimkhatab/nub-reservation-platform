# Nub - Event Reservation Platform

> Modern event reservation platform built with .NET 10, Modular Monolith Architecture, and Domain-Driven Design.

## 🏗️ Architecture

**Pattern:** Modular Monolith with Clean Architecture  
**Framework:** .NET 10 (LTS)  
**Database:** PostgreSQL 16  
**Containerization:** Docker & Docker Compose

### Modules

- **Catalog** - Event management, ticket types, capacity control
- **Bookings** - Reservation transactions with concurrency handling
- **Payments** - Payment processing and refunds
- **Users** - Authentication and authorization
- **Notifications** - Email and SMS delivery _(planned)_

### Architecture Principles

- Clear module boundaries with independent deployment capability
- Domain-Driven Design tactical patterns (Aggregates, Entities, Value Objects)
- CQRS pattern for read/write separation
- Event-driven communication between modules
- Repository pattern with Unit of Work

## 🛠️ Tech Stack

- **.NET 10** - Latest LTS framework
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM with PostgreSQL
- **MediatR** - CQRS implementation
- **FluentValidation** - Input validation
- **Serilog** - Structured logging
- **Docker** - Containerization

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Quick Start
```bash
# Clone repository
git clone https://github.com/mohamedebrahimkhatab/nub-reservation-platform.git
cd nub-reservation-platform

# Start infrastructure
docker-compose up -d

# Run application
dotnet run --project src/API/Nub.API

# Run tests
dotnet test
```

See [Setup Guide](docs/SETUP.md) for detailed instructions.

## 📚 Documentation

- [Architecture Overview](docs/architecture/README.md)
- [Architecture Decisions](docs/architecture/decisions/)
- [Development Setup](docs/SETUP.md)

## 🧪 Testing
```bash
# Unit tests
dotnet test --filter Category=Unit

# Integration tests
dotnet test --filter Category=Integration
```

## 📝 Key Features

- **Concurrent booking handling** - Prevents double-booking with optimistic concurrency
- **Event-driven architecture** - Loose coupling between modules
- **Extensible design** - Easy to add new reservation types (cinema, clinics, etc.)
- **Production-ready** - Logging, health checks, error handling

## 📄 License

MIT License - see [LICENSE](LICENSE) file for details.

---

**Status:** Active Development  
**Started:** January 2026