FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY "Services.api.Roulette.csproj" .
RUN dotnet restore "Services.api.Roulette.csproj"
COPY . .
RUN dotnet build . -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.api.Roulette.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.api.Roulette.dll"]