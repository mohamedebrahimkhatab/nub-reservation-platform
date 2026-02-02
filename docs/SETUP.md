# Development Setup Guide

## Prerequisites

### Required Software
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (LTS)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)
- Code Editor: [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Verify Installation

```bash
# Check .NET version
dotnet --version
# Expected: 10.x.x

# Check Docker
docker --version
docker-compose --version
```

---

## Local Development Setup

### 1. Clone Repository

```bash
git clone https://github.com/mohamedebrahimkhatab/nub-reservation-platform.git
cd nub-reservation-platform
```

### 2. Start Infrastructure

We use Docker Compose to run PostgreSQL locally.

```bash
# Start PostgreSQL
docker-compose up -d

# Verify it's running
docker ps
# You should see PostgreSQL container running on port 5432
```

### 3. Database Setup

```bash
# Navigate to API project
cd src/API/Nub.API

# Apply migrations (creates database schema)
dotnet ef database update

# Verify connection
dotnet ef database list
```

### 4. Run Application

```bash
# From repository root
dotnet run --project src/API/Nub.API

# Or with hot reload (auto-restart on code changes)
dotnet watch run --project src/API/Nub.API
```

Application runs at: `https://localhost:7001` (or check console output)

Swagger UI: `https://localhost:7001/swagger`

### 5. Run Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true

# Run specific test categories
dotnet test --filter Category=Unit
dotnet test --filter Category=Integration
```

---

## VS Code Setup (Recommended)

### Required Extensions

Install these extensions for the best .NET development experience:

1. **C# Dev Kit** (ms-dotnettools.csdevkit)
2. **C#** (ms-dotnettools.csharp)
3. **Docker** (ms-azuretools.vscode-docker)
4. **REST Client** (humao.rest-client)
5. **GitLens** (eamodio.gitlens)
6. **Error Lens** (usernamehw.errorlens)

### Settings

Add to `.vscode/settings.json`:

```json
{
  "dotnet.defaultSolution": "Nub.sln",
  "omnisharp.enableRoslynAnalyzers": true,
  "omnisharp.enableEditorConfigSupport": true,
  "editor.formatOnSave": true,
  "files.trimTrailingWhitespace": true
}
```

---

## Common Commands

### Solution Management

```bash
# Restore NuGet packages
dotnet restore

# Build solution
dotnet build

# Clean build artifacts
dotnet clean

# Format code
dotnet format
```

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName --project src/Modules/Catalog/Nub.Modules.Catalog.Infrastructure

# Update database
dotnet ef database update

# Rollback migration
dotnet ef database update PreviousMigrationName

# Remove last migration (if not applied)
dotnet ef migrations remove
```

### Docker Commands

```bash
# Start all services
docker-compose up -d

# Stop all services
docker-compose down

# View logs
docker-compose logs -f

# Restart specific service
docker-compose restart postgres

# Remove volumes (clean slate)
docker-compose down -v
```

---

## Troubleshooting

### Port Already in Use

If port 5432 (PostgreSQL) is already in use:

```bash
# Find process using the port (Windows)
netstat -ano | findstr :5432

# Kill the process
taskkill /PID <process_id> /F

# Or change port in docker-compose.yml
```

### Database Connection Issues

```bash
# Check PostgreSQL is running
docker ps

# View PostgreSQL logs
docker-compose logs postgres

# Test connection manually
docker exec -it nub-postgres psql -U nubuser -d nubdb
```

### EF Core Migrations Not Working

```bash
# Install/update EF Core tools
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# Verify installation
dotnet ef --version
```

### Build Errors

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

---

## Environment Variables

Create `.env` file in repository root (gitignored):

```env
# Database
POSTGRES_USER=nubuser
POSTGRES_PASSWORD=nubpassword
POSTGRES_DB=nubdb

# Application
ASPNETCORE_ENVIRONMENT=Development
JWT_SECRET=your-super-secret-key-change-this-in-production
```

---

## Useful Resources

- [.NET 10 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Docker Documentation](https://docs.docker.com/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)

---

## Getting Help

- Check existing [GitHub Issues](https://github.com/mohamedebrahimkhatab/nub-reservation-platform/issues)
- Review [Architecture Documentation](./architecture/README.md)
- Open a new issue for bugs or questions

---

*Last Updated: January 31, 2026*