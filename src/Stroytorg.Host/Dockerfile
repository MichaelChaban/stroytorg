#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stroytorg.Host/Stroytorg.Host.csproj", "Stroytorg.Host/"]
COPY ["Stroytorg.Application/Stroytorg.Application.csproj", "Stroytorg.Application/"]
COPY ["Stroytorg.Contracts/Stroytorg.Contracts.csproj", "Stroytorg.Contracts/"]
COPY ["Stroytorg.Infrastructure/Stroytorg.Infrastructure.csproj", "Stroytorg.Infrastructure/"]
COPY ["Stroytorg.Domain/Stroytorg.Domain.csproj", "Stroytorg.Domain/"]
RUN dotnet restore "./Stroytorg.Host/./Stroytorg.Host.csproj"
COPY . .
WORKDIR "/src/Stroytorg.Host"
RUN dotnet build "./Stroytorg.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Stroytorg.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stroytorg.Host.dll"]