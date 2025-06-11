FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/GameService/GameService.csproj ./GameService/
WORKDIR /app/GameService
RUN dotnet restore

COPY src/GameService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/GameService/out ./
ENTRYPOINT ["dotnet", "GameService.dll"]