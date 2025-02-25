FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SushiGo.Orders/Playground/Playground.csproj", "SushiGo.Orders/Playground/"]
RUN dotnet restore "SushiGo.Orders/Playground/Playground.csproj"
COPY . .
WORKDIR "/src/SushiGo.Orders/Playground"
RUN dotnet build "Playground.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Playground.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playground.dll"]
