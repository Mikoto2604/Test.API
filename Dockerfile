FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PaymentApp.Api/PaymentApp.Api.csproj", "PaymentApp.Api/"]
COPY ["PaymentApp.Application/PaymentApp.Application.csproj", "PaymentApp.Application/"]
COPY ["PaymentApp.Domain/PaymentApp.Domain.csproj", "PaymentApp.Domain/"]
COPY ["PaymentApp.Infrastructure/PaymentApp.Infrastructure.csproj", "PaymentApp.Infrastructure/"]
RUN dotnet restore "./PaymentApp.Api/PaymentApp.Api.csproj"
COPY . .
WORKDIR "/src/PaymentApp.Api"
RUN dotnet build "./PaymentApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PaymentApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentApp.Api.dll"]