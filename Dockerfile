FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/WarehouseApi.Api/WarehouseApi.Api.csproj", "src/WarehouseApi.Api/"]
COPY ["src/WarehouseApi.Infrastructure/WarehouseApi.Infrastructure.csproj", "src/WarehouseApi.Infrastructure/"]
COPY ["src/WarehouseApi.Application/WarehouseApi.Application.csproj", "src/WarehouseApi.Application/"]
COPY ["src/WarehouseApi.Domain/WarehouseApi.Domain.csproj", "src/WarehouseApi.Domain/"]

RUN dotnet restore "src/WarehouseApi.Api/WarehouseApi.Api.csproj"

COPY . .

RUN dotnet publish "src/WarehouseApi.Api/WarehouseApi.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WarehouseApi.Api.dll"]
