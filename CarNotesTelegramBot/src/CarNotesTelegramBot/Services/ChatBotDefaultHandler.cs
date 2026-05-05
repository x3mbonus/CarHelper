// -----------------------------------------------------------------------
// <copyright file="ChatBotImageHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using CarNotesTelegramBot.DbContexts;
using CarNotesTelegramBot.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CarNotesTelegramBot.Services;

public class ChatBotDefaultHandler(
    ILogger<ChatBotDefaultHandler> logger,
    AppDbContext dbContext)
    : IChatBotDefaultHandler
{
    public async Task HandleOtherMessagesAsync(
        ITelegramBotClient botClient,
        Message? message,
        CancellationToken cancellationToken)
    {
        if (message is null)
        {
            return;
        }

        var chatId = message.Chat.Id;

        var (carId, error) = TryParseCarId(message.Text);
        if (!string.IsNullOrEmpty(error))
        {
            await LogAndSendError(error, botClient, chatId, cancellationToken);
        }

        if (string.IsNullOrEmpty(carId))
        {
            return;
        }

        var carData = await dbContext.CarData
            .Where(x => x.CarId == carId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        var carFeatures = await dbContext.CarFeatures
            .Include(x => x.Feature)
            .Where(x => x.CarId == carId)
            .AsNoTracking()
            .OrderBy(x => x.Feature.Sort)
            .ToListAsync(cancellationToken);


        var response = new StringBuilder();

        // Header
        response.AppendLine("📋 <b>CAR DETAILS</b>");
        response.AppendLine();

        // Notes section
        if (!string.IsNullOrWhiteSpace(carData?.Text))
        {
            response.AppendLine("<b>NOTES</b>");
            response.AppendLine(carData.Text);
            response.AppendLine();
        }

        // Verification Status
        response.AppendLine("<b>VERIFICATION STATUS</b>");
        var verificationStatus = carData?.Confirmed == true ? "✅ Verified" : "⏳ Not Verified";
        response.AppendLine(verificationStatus);
        response.AppendLine();

        // Car Details - Make and Model
        response.AppendLine("<b>CAR INFORMATION</b>");
        if (!string.IsNullOrWhiteSpace(carData?.Make) || !string.IsNullOrWhiteSpace(carData?.Model))
        {
            response.AppendLine($"<b>Make:</b> {carData?.Make}");
            response.AppendLine($"<b>Model:</b> {carData?.Model}");
        }

        // Price Information
        if (!string.IsNullOrWhiteSpace(carData?.Price) || !string.IsNullOrWhiteSpace(carData?.PriceEur))
        {
            response.AppendLine();
            if (!string.IsNullOrWhiteSpace(carData?.Price))
            {
                response.AppendLine($"<b>Price (PLN):</b> {carData.Price}");
            }
            if (!string.IsNullOrWhiteSpace(carData?.PriceEur))
            {
                response.AppendLine($"<b>Price (EUR):</b> {carData.PriceEur}");
            }
        }

        // Mileage and Year
        response.AppendLine();
        if (!string.IsNullOrWhiteSpace(carData?.Mileage))
        {
            response.AppendLine($"<b>Mileage (KM):</b> {carData.Mileage}");
        }
        if (!string.IsNullOrWhiteSpace(carData?.Year))
        {
            response.AppendLine($"<b>Year:</b> {carData.Year}");
        }

        // Address
        if (!string.IsNullOrWhiteSpace(carData?.Address))
        {
            response.AppendLine();
            response.AppendLine($"<b>Address:</b> {carData.Address}");
        }

        // Features section
        if (carFeatures.Count > 0)
        {
            response.AppendLine();
            response.AppendLine("<b>FEATURES</b>");
            foreach (var carFeature in carFeatures)
            {
                string featureStatus;

                if (carFeature.State == true)
                {
                    featureStatus = "✅"; // Present
                }
                else
                {
                    // Missing (false) or Unknown (null) - show priority-based importance
                    var priority = carFeature.Feature.Priority ?? 0;
                    var isMissing = carFeature.State == false;

                    // Determine color based on priority
                    string colorIndicator;
                    if (priority >= 3) // High priority
                    {
                        colorIndicator = "🔴";
                    }
                    else if (priority >= 1) // Normal priority
                    {
                        colorIndicator = "🟡";
                    }
                    else // Low priority
                    {
                        colorIndicator = "⚪";
                    }

                    // Add missing/unknown indicator
                    featureStatus = isMissing ? $"{colorIndicator}" : $"{colorIndicator}❓";
                }

                response.AppendLine($"{featureStatus} {carFeature.Feature.Label}");
            }
        }

        response.AppendLine();
        response.AppendLine("═══════════════════════════════════════");

        await botClient.SendMessage(
            chatId,
            response.ToString(),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);
    }

    private (string? CarId, string Error) TryParseCarId(string? messageText)
    {
        if (messageText is null ||
            !messageText.Contains("https://", StringComparison.OrdinalIgnoreCase))
        {
            return (null, string.Empty);
        }

        var urlText = $"https://{messageText.Split("https://").LastOrDefault()}";
        urlText = urlText.Trim();

        // Try parse as URI
        if (!Uri.TryCreate(urlText, UriKind.Absolute, out var uri))
        {
            return (null, "Invalid URL");
        }

        // 1. Try query param ?id=XXXX (mobile.de)
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        var id = query["id"];
        if (!string.IsNullOrWhiteSpace(id))
        {
            return (id, string.Empty);
        }

        // 2. Try otomoto pattern: ...-ID6HXt9b.html
        var path = uri.AbsolutePath;

        var match = System.Text.RegularExpressions.Regex.Match(
            path,
            @"-(ID[A-Za-z0-9]+)\.html",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return (match.Groups[1].Value, string.Empty);
        }

        // 3. Try mobile.de numeric pattern: .../NUMBERS.html
        match = System.Text.RegularExpressions.Regex.Match(
            path,
            @"/(\d+)\.html",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return (match.Groups[1].Value, string.Empty);
        }

        return (null, "Car ID not found in URL");
    }

    private async Task LogAndSendError(
        string message,
        ITelegramBotClient botClient,
        long chatId,
        CancellationToken cancellationToken)
    {
        logger.LogError(message);
        await botClient.SendMessage(chatId, $"❌ {message}.",
            cancellationToken: cancellationToken);
    }

}