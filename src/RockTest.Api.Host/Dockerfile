#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RockTest.Api.Host/RockTest.Api.Host.csproj", "RockTest.Api.Host/"]
COPY ["RockTest.Core/RockTest.Core.csproj", "RockTest.Core/"]
COPY ["RockTest.Api.Host.Contracts/RockTest.Api.Host.Contracts.csproj", "RockTest.Api.Host.Contracts/"]
COPY ["RockTest.Common/RockTest.Common.csproj", "RockTest.Common/"]
COPY ["RockTest.Games/RockTest.Games.csproj", "RockTest.Games/"]
COPY ["RockTest.Games.Contracts/RockTest.Games.Contracts.csproj", "RockTest.Games.Contracts/"]
COPY ["RockTest.Rand.Contracts/RockTest.Rand.Contracts.csproj", "RockTest.Rand.Contracts/"]
COPY ["RockTest.Rand/RockTest.Rand.csproj", "RockTest.Rand/"]
COPY ["RockTest.Services/RockTest.Services.csproj", "RockTest.Services/"]
COPY ["RockTest.Services.Contracts/RockTest.Services.Contracts.csproj", "RockTest.Services.Contracts/"]
RUN dotnet restore "./RockTest.Api.Host/RockTest.Api.Host.csproj"
COPY . .
WORKDIR "/src/RockTest.Api.Host"
RUN dotnet build "./RockTest.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RockTest.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RockTest.Api.Host.dll"]