<div>

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE.txt)
[![C#](https://img.shields.io/badge/language-C%23-239120.svg)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4.svg)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-enabled-2496ED.svg)](https://www.docker.com/)

</div>

# ForgeCore

A modular, self-hosted gaming backend built with **ASP.NET Core 10**.

> **ForgeCore is still under active development and is not production ready.**
> Expect breaking changes, incomplete features and frequent refactoring.

![Hero](https://raw.githubusercontent.com/Rotwo/ForgeCore/refs/heads/master/.github/assets/hero.jpg)

Instead of relying on proprietary backend platforms, the goal is to offer an open-source alternative that developers can understand, modify and deploy on their own infrastructure.

## Current modules

- Authentication
- Player management
- Inventories
- Economy
- Social (In-work)
- Moderación (Next)

The architecture is modular, so new services can be added over time.

## Tech stack

- .NET 10
- ASP.NET Core
- PostgreSQL
- Redis
- Entity Framework Core
- Docker

## Running locally

Clone the repository:

```bash
git clone https://github.com/Rotwo/ForgeCore.git
cd ForgeCore
```

Start the infrastructure:

```bash
docker compose up -d
```

Then run the Gateway:

```bash
dotnet run --project ForgeCore.Gateway
```

Swagger will be available at:

```
http://localhost:5065/swagger
```

## Contributing

Contributions are more than welcome.

Whether it's fixing a bug, improving the architecture, writing documentation, reporting issues or proposing new ideas, every contribution helps move the project forward.

If you'd like to contribute:

1. Fork the repository.
2. Create a feature branch.
3. Make your changes.
4. Open a Pull Request.

Feel free to start a discussion if you have questions or want feedback before implementing something.

## Documentation

- API documentation: `/docs.md`
- SDKs: `/sdks`

## License

ForgeCore is licensed under the Apache 2.0 License.
See [LICENSE.txt](LICENSE.txt) for details.