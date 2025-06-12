FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy .csproj files only
COPY src/UserService/UserService.csproj ./UserService/
COPY libraries/GameHub.Common.Auth/GameHub.Common.Auth.csproj ./libraries/GameHub.Common.Auth/

WORKDIR /src/UserService
RUN dotnet restore

COPY src/UserService/ ./UserService/
COPY libraries/GameHub.Common.Auth/ ../libraries/GameHub.Common.Auth/

WORKDIR /app/UserService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/UserService/out .
ENTRYPOINT ["dotnet", "UserService.dll"]