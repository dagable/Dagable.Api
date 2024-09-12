# Use the ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Adjust paths since the Dockerfile is in Dagable.Api
COPY ["Dagable.Api/Dagable.Api.csproj", "Dagable.Api/"]
COPY ["..//Dagable.Api.Core/Dagable.Api.Core.csproj", "..//Dagable.Api.Core/"]
COPY ["..//Dagable.ErrorManagement/Dagable.ErrorManagement.csproj", "..//Dagable.ErrorManagement/"]
COPY ["..//Dagable.Api.Services/Dagable.Api.Services.csproj", "..//Dagable.Api.Services/"]
COPY ["..//Dagable.DataAccess/Dagable.DataAccess.csproj", "..//Dagable.DataAccess/"]
COPY ["../Dagable.DataAccess.Migrations/Dagable.DataAccess.Migrations.csproj", "../Dagable.DataAccess.Migrations/"]

# Restore dependencies
RUN dotnet restore "./Dagable.Api/Dagable.Api.csproj"

# Copy the remaining files
COPY .. .

# Build the application
WORKDIR "/src/Dagable.Api"
RUN dotnet build "./Dagable.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./Dagable.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage for the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dagable.Api.dll"]