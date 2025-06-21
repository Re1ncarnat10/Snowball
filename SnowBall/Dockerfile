FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 7081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SnowBall/SnowBall.csproj", "./"]
RUN dotnet restore "SnowBall.csproj"
COPY SnowBall/. .
RUN dotnet build "SnowBall.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SnowBall.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y libpq-dev
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnowBall.dll"]
