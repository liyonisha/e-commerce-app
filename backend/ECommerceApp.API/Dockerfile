FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ECommerceApp.API/ECommerceApp.API.csproj", "ECommerceApp.API/"]
COPY ["ECommerceApp.Application/ECommerceApp.Application.csproj", "ECommerceApp.Application/"]
COPY ["ECommerceApp.Domain/ECommerceApp.Domain.csproj", "ECommerceApp.Domain/"]
COPY ["ECommerceApp.Infrastructure/ECommerceApp.Infrastructure.csproj", "ECommerceApp.Infrastructure/"]

RUN dotnet restore "ECommerceApp.API/ECommerceApp.API.csproj"

COPY . .
WORKDIR "/src/ECommerceApp.API"
RUN dotnet build "ECommerceApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApp.API.dll"]
