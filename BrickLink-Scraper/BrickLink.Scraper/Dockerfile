FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrickLink.Scraper/BrickLink.Scraper.csproj", "BrickLink.Scraper/"]
RUN dotnet restore "BrickLink.Scraper/BrickLink.Scraper.csproj"
COPY . .
WORKDIR "/src/BrickLink.Scraper"
RUN dotnet build "BrickLink.Scraper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrickLink.Scraper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrickLink.Scraper.dll"]
