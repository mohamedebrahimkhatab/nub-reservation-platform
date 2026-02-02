# Nub - Event Reservation Platform

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?logo=postgresql)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

> A modern event reservation platform built with Modular Monolith Architecture, demonstrating Clean Architecture, DDD, and CQRS patterns.

## 🎯 Project Goals

### Technical Goals
- Master **Modular Monolith** architecture patterns
- Apply **Clean Architecture** and **Domain-Driven Design** principles
- Implement **CQRS** and **Event-Driven** communication
- Build production-ready .NET backend with best practices
- Level up from mid-level to **senior .NET developer**

### Business Goals
- Create a competitive event reservation platform for the Egyptian market
- Provide better UX than existing solutions (Tazkarti, Ticket Marchee)
- Build scalable foundation for future expansion (cinema, clinics, transportation)

## 🏗️ Architecture

- **Pattern:** Modular Monolith with Clean Architecture
- **Framework:** .NET 10 (LTS)
- **Database:** PostgreSQL 16
- **Containerization:** Docker & Docker Compose
- **Communication:** Domain Events, MediatR
- **Testing:** xUnit, FluentAssertions

### Modules

1. **Catalog Module** - Event management, ticket types, capacity
2. **Bookings Module** - Reservation transactions, concurrency handling
3. **Payments Module** - Payment processing, refunds
4. **Users Module** - Authentication, user management, roles
5. **Notifications Module** _(future)_ - Email, SMS confirmations

## 📚 Documentation

- [Architecture Decisions](docs/architecture/README.md)
- [Learning Roadmap](docs/LEARNING_ROADMAP.md)
- [Session Notes](docs/sessions/)
- [Development Setup](docs/SETUP.md)

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

### Quick Start
```bash
# Clone the repository
git clone https://github.com/mohamedebrahimkhatab/nub-reservation-platform.git
cd nub-reservation-platform

# Start infrastructure (PostgreSQL)
docker-compose up -d

# Run the application
dotnet run --project src/API/Nub.API

# Run tests
dotnet test
```

## 📖 Learning Journey

This project documents my journey from mid-level to senior .NET developer. Each commit, decision, and refactoring is intentional and educational.

Key learning areas:
- ✅ Modular Monolith boundaries and communication
- ✅ Clean Architecture dependency rules
- ✅ Domain-Driven Design tactical patterns
- ✅ CQRS and Event Sourcing
- ✅ Docker containerization
- ✅ PostgreSQL advanced features
- ✅ CI/CD pipelines
- ✅ Performance optimization and caching

## 🛠️ Tech Stack

**Backend:**
- .NET 10, C# 13
- ASP.NET Core Web API
- Entity Framework Core 10
- MediatR (CQRS)
- FluentValidation
- Serilog (Logging)

**Database:**
- PostgreSQL 16
- Npgsql (EF Core provider)

**Infrastructure:**
- Docker & Docker Compose
- Redis (Caching)
- RabbitMQ (Message Broker - future)

**Testing:**
- xUnit
- FluentAssertions
- Testcontainers
- Moq

## 📝 Project Status

**Current Phase:** Foundation Setup  
**Started:** January 2026  
**Target MVP:** Q2 2026

## 🤝 Contributing

This is a personal learning project, but feedback and suggestions are welcome! Feel free to open issues or discussions.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

Built with guidance and mentorship, focusing on real-world patterns and best practices used in production .NET applications.

---

**Made with ❤️ in Egypt**