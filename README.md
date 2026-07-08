
<div>

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE.txt)
[![C#](https://img.shields.io/badge/language-C%23-239120.svg)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4.svg)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-enabled-2496ED.svg)](https://www.docker.com/)

</div>

# Forgecore

A high-performance, self-hosted gaming backend framework built with **ASP.NET Core 10** and **C#**. Designed for game studios and indie developers who want full control over their game server infrastructure.

![Hero](https://raw.githubusercontent.com/Rotwo/ForgeCore/refs/heads/master/.github/assets/hero.jpg)

[Features](#features) • [Quick Start](#quick-start) • [Documentation](#documentation) • [Contributing](#contributing)

---

## Overview

ForgeCore is a modular, production-ready gaming backend that provides essential services for online games. Built on the .NET platform with containerization support, it's ready to deploy on your own infrastructure.

**Perfect for:**
- Indie game studios
- Custom game backends
- Self-hosted online solutions
- Game developers who value infrastructure control

## Features
### 🔐 Core Services
- **Authentication** (`ForgeCore.Auth`)
  - JWT token-based authentication
  - Multi-provider support
  - Session management
  - Account management

- **Player Management** (`ForgeCore.Players`)
  - Used in other services as identification

- **Inventory System** (`ForgeCore.Inventories`)
  - Flexible item management
  - Inventory operations
  - Item persistence

- **Economy System** (`ForgeCore.Economy`)
  - Wallet management
  - Transaction handling
  - Supports multiple balances

### 🛠️ Infrastructure
- **PostgreSQL** - Persistent data storage
- **Redis** - Cache handling
- **Docker & Docker Compose** - Container orchestration
- **Entity Framework Core** - ORM with migrations
- **Swagger** - API docs

### 🚀 Modern Stack
- **.NET 10** - Latest framework features
- **Postgres 16** - Reliable data persistance

## Quick Start
### Prerequisites
- [Docker](https://www.docker.com/) & Docker Compose
- OR [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) for local development

### Option 1: Docker Compose (Recommended)

1. **Clone the repository**
```bash
git clone https://github.com/Rotwo/ForgeCore.git
cd ForgeCore
```

2. **Configure environment variables**
```bash
# Edit docker-compose.yml and update the JWT secret
# Look for: Jwt__Key=replace_this_with_a_secure_random_value_of_sufficient_length
```

3. **Start the services**
```bash
docker-compose up -d
```

### Option 2: Local Development

1. **Prerequisites**
```bash
# Install dependencies
dotnet restore
```

2. **Database Setup**
```bash
# Ensure PostgreSQL is running locally
# Update connection string in appsettings.Development.json
dotnet ef database update --project ForgeCore.Infrastructure --startup-project ForgeCore.Gateway
```

3. **Run the Gateway**
```bash
cd ForgeCore.Gateway
dotnet run
```

### Verification

After starting the services, verify everything is working:

```bash
# Access Swagger UI
open http://localhost:5065/swagger
```

## Configuration

### Environment Variables

Key configuration options (set in `docker-compose.yml` or environment):

| Variable | Default Value | Description |
|----------|---------------|-------------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Execution mode: Development, Staging, Production |
| `ASPNETCORE_URLS` | `http://+:8080` | Server binding address and port |
| `ConnectionStrings__ForgeCoreDb` | `Host=postgres;Port=5432;Database=forgecore;Username=forgecore;Password=forgecore` | PostgreSQL connection string |
| `Jwt__Key` | `your-secure-random-key-here` | **⚠️ MUST be changed in production** - JWT signing key |
| `Jwt__Issuer` | `ForgeCore` | Token issuer identifier |
| `Jwt__Audience` | `ForgeCoreClient` | Token audience identifier |
| `Jwt__ExpiryMinutes` | `60` | Token expiration time in minutes |
| `Redis__ConnectionString` | `redis:6379` | Redis connection string |

## Development

### Building from Source

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run tests (when added)
dotnet test
```

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add YourMigrationName --project ForgeCore.Infrastructure --startup-project ForgeCore.Gateway

# Apply migrations
dotnet ef database update --project ForgeCore.Infrastructure --startup-project ForgeCore.Gateway

# Rollback last migration
dotnet ef migrations remove --project ForgeCore.Infrastructure --startup-project ForgeCore.Gateway
```

### Code Style

This project follows C# conventions with:
- Nullable reference types enabled
- Implicit usings
- Modern C# patterns

## Project Structure

```
ForgeCore/
├── ForgeCore.Auth/              # Authentication & Authorization
│   ├── Application/             # Business logic & services
│   └── Contracts/               # DTOs & interfaces
├── ForgeCore.Players/           # Player management service
├── ForgeCore.Inventories/       # Inventory service
├── ForgeCore.Economy/           # Economy & Wallet service
├── ForgeCore.Infrastructure/    # Database & Repositories
│   ├── Persistence/             # EF Core context & migrations
│   └── Repositories/            # Data access layer
├── ForgeCore.Shared/            # Shared models & dto's
├── ForgeCore.Gateway/           # API Gateway (ASP.NET Core)
│   ├── Controllers/             # API controllers
│   ├── Middleware/              # Custom middlewares
│   └── Program.cs               # Startup configuration
├── docker-compose.yml           # Multi-container orchestration
└── Dockerfile                   # Container image
```

## Showcase
[Test on Unity 6.4 using Experimental SDK](https://raw.githubusercontent.com/Rotwo/ForgeCore/refs/heads/master/.github/assets/unity_example.mp4)

## Contributing

We welcome contributions! Here's how to get started:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Contribution Guidelines
- Follow C# naming conventions
- Include unit tests for new features
- Update documentation as needed
- Ensure code builds and passes tests

## License

This project is licensed under the **Apache License 2.0** - see the [LICENSE.txt](LICENSE.txt) file for details.

## Documentation

- [API Documentation](/docs.md)
- [Engines SDKs](/sdks)

## Support

For issues, questions, or suggestions:
- [Open an Issue](https://github.com/Rotwo/ForgeCore/issues)
- [Start a Discussion](https://github.com/Rotwo/ForgeCore/discussions)

<div align="center">
<br>

**Made with ❤️ for game developers**
</div>
