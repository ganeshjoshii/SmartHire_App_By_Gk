# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY SmartHr/SmartHr.csproj SmartHr/
RUN dotnet restore SmartHr/SmartHr.csproj

# Copy rest of the code
COPY SmartHr/ SmartHr/

# Publish app
RUN dotnet publish SmartHr/SmartHr.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SmartHr.dll"]
