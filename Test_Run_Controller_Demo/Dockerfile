# syntax=docker/dockerfile:1

# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first so the NuGet layer is cached until the project file changes
COPY Test_Run_Controller/Test_Run_Controller.csproj Test_Run_Controller/
RUN dotnet restore "Test_Run_Controller/Test_Run_Controller.csproj"

# Copy the rest of the source and publish a Release build
COPY Test_Run_Controller/ Test_Run_Controller/
RUN dotnet publish "Test_Run_Controller/Test_Run_Controller.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Listen on port 8080 inside the container
ENV ASPNETCORE_URLS=http://+:8080
# Development enables Swagger UI (see Program.cs). Override with -e ASPNETCORE_ENVIRONMENT=Production to disable it.
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Test_Run_Controller.dll"]
