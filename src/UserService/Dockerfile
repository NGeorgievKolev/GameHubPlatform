FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/UserService/UserService.csproj ./UserService/
WORKDIR /app/UserService
RUN dotnet restore

COPY src/UserService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/UserService/out ./
ENTRYPOINT ["dotnet", "UserService.dll"]