# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.301 AS build
WORKDIR /src

# Salin semua file project ke container
COPY . .

# Masuk ke folder yang berisi .csproj
WORKDIR /src/CatatanKeuangan/CatatanKeuangan

# Restore dependency dan publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.301
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "CatatanKeuangan.dll"]