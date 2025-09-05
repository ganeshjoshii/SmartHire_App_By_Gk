# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj first (layer caching ke liye)
COPY BackEnd/SmartHr/*.csproj BackEnd/SmartHr/
RUN dotnet restore BackEnd/SmartHr/SmartHr.csproj

# Copy all source code
COPY BackEnd/SmartHr/ BackEnd/SmartHr/

# Publish into /app/publish
RUN dotnet publish BackEnd/SmartHr/SmartHr.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port (change if needed)
EXPOSE 5000

# Run your app
ENTRYPOINT ["dotnet", "SmartHr.dll"]
