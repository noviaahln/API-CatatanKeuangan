# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.301 AS build
WORKDIR /src

# Copy semua isi project ke container
COPY . .

# Restore dan publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0.301
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CatatanKeuangan.dll"]