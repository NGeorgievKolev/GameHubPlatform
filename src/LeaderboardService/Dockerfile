FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/LeaderboardService/LeaderboardService.csproj ./LeaderboardService/
WORKDIR /app/LeaderboardService
RUN dotnet restore

COPY src/LeaderboardService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/LeaderboardService/out ./
ENTRYPOINT ["dotnet", "LeaderboardService.dll"]