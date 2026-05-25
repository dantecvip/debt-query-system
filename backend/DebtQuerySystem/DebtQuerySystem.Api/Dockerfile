# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /backend

COPY . .

RUN dotnet restore "DebtQuerySystem/DebtQuerySystem.Api/DebtQuerySystem.Api.csproj"

RUN dotnet publish "DebtQuerySystem/DebtQuerySystem.Api/DebtQuerySystem.Api.csproj" \
    -c Release \
    -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DebtQuerySystem.Api.dll"]