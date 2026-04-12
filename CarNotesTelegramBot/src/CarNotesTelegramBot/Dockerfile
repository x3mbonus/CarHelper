FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y libleptonica-dev tesseract-ocr && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TelegramBotUsdCounter/TelegramBotUsdCounter.csproj", "src/TelegramBotUsdCounter/"]
RUN dotnet restore "src/TelegramBotUsdCounter/TelegramBotUsdCounter.csproj"
COPY . .
WORKDIR "/src/src/TelegramBotUsdCounter"
RUN dotnet build "TelegramBotUsdCounter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TelegramBotUsdCounter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN dotnet --info
ENTRYPOINT ["dotnet", "TelegramBotUsdCounter.dll"]
