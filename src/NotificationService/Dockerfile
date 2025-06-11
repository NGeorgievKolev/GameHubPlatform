FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/NotificationService/NotificationService.csproj ./NotificationService/
WORKDIR /app/NotificationService
RUN dotnet restore

COPY src/NotificationService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/NotificationService/out ./
ENTRYPOINT ["dotnet", "NotificationService.dll"]