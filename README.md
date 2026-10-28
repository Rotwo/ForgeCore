# ForgeCore

> [!WARNING]
> Currently the development of this repo has been paused. <br>
> It is expected to be resumed later this year.

An open-source, modular backend for online games built with ASP.NET Core.

> **Early development. Not production ready. Breaking changes are expected.**

## Current modules

- Authentication
- Player management
- Inventories
- Economy

## Running

```bash
git clone https://github.com/Rotwo/ForgeCore.git
cd ForgeCore

docker compose up -d
dotnet run --project ForgeCore.Gateway
