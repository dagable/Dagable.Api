#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-focal-arm64v8 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.408-focal-arm64v8 AS build
WORKDIR /src
COPY ["Dagable.Api/Dagable.Api.csproj", "Dagable.Api/"]
COPY ["Dagable.Api.Services/Dagable.Api.Services.csproj", "Dagable.Api.Services/"]
COPY ["Dagable.Api.Core/Dagable.Api.Core.csproj", "Dagable.Api.Core/"]
COPY packages/. packages/.
RUN dotnet restore "Dagable.Api/Dagable.Api.csproj" -s /src/packages -s https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/Dagable.Api"
RUN dotnet build "Dagable.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dagable.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dagable.Api.dll"]