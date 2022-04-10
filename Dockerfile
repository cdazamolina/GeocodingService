#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Boostrapers/GeocodingService.Api/GeocodingService.Api.csproj", "Boostrapers/GeocodingService.Api/"]
COPY ["src/Infraestructure/GeocodingService.Infraestructure/GeocodingService.Infraestructure.csproj", "Infraestructure/GeocodingService.Infraestructure/"]
COPY ["src/Core/GeocodingService.Core/GeocodingService.Core.csproj", "Core/GeocodingService.Core/"]
RUN dotnet restore "Boostrapers/GeocodingService.Api/GeocodingService.Api.csproj"

COPY . .
WORKDIR "src/Boostrapers/GeocodingService.Api"
RUN dotnet build "GeocodingService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeocodingService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeocodingService.Api.dll"]