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
        if (string.IsNullOrEmpty(carId))
        {
            await LogAndSendError(error, botClient, chatId, cancellationToken);
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
        response.AppendLine("<b>Your Car data:</b>");
        response.AppendLine($"{carData?.Model}");
        foreach (var carFeature in carFeatures)
        {
            response.AppendLine($"{carFeature.Feature.Label}: {carFeature.State}");
        }

        response.AppendLine("--------------------------------------------------------------------------------------------");
        await botClient.SendMessage(
            chatId,
            response.ToString(),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);
    }

    private (string? CarId, string Error) TryParseCarId(string? messageText)
    {
        if (string.IsNullOrWhiteSpace(messageText))
        {
            return (null, "Message is empty");
        }

        // Try parse as URI
        if (!Uri.TryCreate(messageText.Trim(), UriKind.Absolute, out var uri))
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