# CarHelper

A Telegram bot that accepts car listing links, parses and stores car data (including OCR-based feature extraction).

> **CarHelperApi** — unfinished, not in use.

## Tech Stack

- **.NET 8.0**
- **Entity Framework Core 9** with Npgsql (PostgreSQL)
- **Telegram.Bot 22.x** — bot integration
- **Tesseract OCR + libleptonica** — image-based feature extraction
- **Docker / Docker Compose** — containerized deployment

## Project Structure

```
CarNotesTelegramBot/
├── src/CarNotesTelegramBot/
│   ├── Services/          # Bot handlers (start, default, feature)
│   ├── Entities/          # Car data, features, feature config entities
│   └── DbContexts/        # EF Core DbContext
└── docker-compose.yml
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- A Telegram bot token (from [@BotFather](https://t.me/BotFather))
- PostgreSQL (or use the Docker Compose setup)

## Getting Started

Create a `.env` file in `CarNotesTelegramBot/` (never commit this):

```env
TELEGRAM_BOT_TOKEN=your_bot_token_here
```

Set authorized user IDs in `appsettings.json`:

```json
{
  "TelegramBot": {
    "AuthorizedUserIds": [123456789]
  }
}
```

**Run with Docker Compose:**

```bash
cd CarNotesTelegramBot
docker-compose up -d
```

**Run locally:**

```bash
cd CarNotesTelegramBot/src/CarNotesTelegramBot
dotnet run
```

## Telegram Bot Commands

| Command | Description |
|---|---|
| `/start` | Prompts the user to send car listing links |

After `/start`, send a car listing URL and the bot will parse, extract, and store the car data including make, model, year, price, mileage, condition, and features.

## Database Schema

- **car_data** — full car records (make, model, price, year, mileage, address, condition, etc.)
- **features_config** — feature definitions (key-value pairs with sort priority)
- **car_features** — many-to-many mapping between car records and features

## Security Notes

- Keep your Telegram bot token out of source control — use `.env` or user secrets.
- The bot checks incoming messages against an authorized user ID list; only listed users can interact with it.
