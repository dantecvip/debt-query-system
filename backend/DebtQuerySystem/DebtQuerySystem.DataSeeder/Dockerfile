# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /backend

COPY . .

RUN dotnet restore "DebtQuerySystem/DebtQuerySystem.DataSeeder/DebtQuerySystem.DataSeeder.csproj"

COPY . .

RUN dotnet publish "DebtQuerySystem/DebtQuerySystem.DataSeeder/DebtQuerySystem.DataSeeder.csproj" \
    -c Release \
    -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends libgdiplus libc6-dev && rm -rf /var/lib/apt/lists/*

COPY --from=build-env /app/publish .

RUN mkdir -p /app/data

ENTRYPOINT ["dotnet", "DebtQuerySystem.DataSeeder.dll"]