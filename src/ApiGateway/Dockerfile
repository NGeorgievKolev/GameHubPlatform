FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/ApiGateway/ApiGateway.csproj ./ApiGateway/
WORKDIR /app/ApiGateway
RUN dotnet restore

COPY src/ApiGateway/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/ApiGateway/out ./
ENTRYPOINT ["dotnet", "ApiGateway.dll"]