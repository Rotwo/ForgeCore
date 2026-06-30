FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ForgeCore.Gateway/ForgeCore.Gateway.csproj", "ForgeCore.Gateway/"]
COPY ["ForgeCore.Auth/ForgeCore.Auth.csproj", "ForgeCore.Auth/"]
COPY ["ForgeCore.Infrastructure/ForgeCore.Infrastructure.csproj", "ForgeCore.Infrastructure/"]
COPY ["ForgeCore.Players/ForgeCore.Players.csproj", "ForgeCore.Players/"]
COPY ["ForgeCore.Shared/ForgeCore.Shared.csproj", "ForgeCore.Shared/"]
RUN dotnet restore "ForgeCore.Gateway/ForgeCore.Gateway.csproj"

COPY . .
RUN dotnet publish "ForgeCore.Gateway/ForgeCore.Gateway.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ForgeCore.Gateway.dll"]
