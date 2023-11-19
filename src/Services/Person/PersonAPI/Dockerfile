#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Person/PersonAPI/PersonAPI.csproj", "src/Services/Person/PersonAPI/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus/"]
COPY ["src/Services/Person/Application/Application.csproj", "src/Services/Person/Application/"]
COPY ["src/Services/Person/Domain/Domain.csproj", "src/Services/Person/Domain/"]
COPY ["src/Services/Person/Infrastructure/Infrastructure.csproj", "src/Services/Person/Infrastructure/"]
RUN dotnet restore "./src/Services/Person/PersonAPI/./PersonAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Person/PersonAPI"
RUN dotnet build "./PersonAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PersonAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonAPI.dll"]