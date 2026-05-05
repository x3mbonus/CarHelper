// -----------------------------------------------------------------------
// <copyright file="ChatBotEntry.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CarNotesTelegramBot.Interfaces;
using CarNotesTelegramBot.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Services;

public class ChatBotEntry(
    ILogger<ChatBotEntry> logger,
    IOptions<TelegramBotSettings> telegramBotSettings,
    IServiceScopeFactory serviceScopeFactory) : IChatBotEntry
{
    public void StartBot()
    {
        var botToken = telegramBotSettings.Value.CarNotesToken;

        if (string.IsNullOrEmpty(botToken))
        {
            logger.LogError("Bot token is missing! Please set it in appsettings.json, user secrets, or environment variables.");
            return;
        }

        var botClient = new TelegramBotClient(botToken);

        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = []
        };


        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token
        );

        logger.LogInformation("Bot started...");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Get user ID from update
        var userId = update.Message?.From?.Id
                  ?? update.CallbackQuery?.From?.Id
                  ?? update.EditedMessage?.From?.Id;
        if (userId is null)
        {
            logger.LogWarning("Update received without user ID");
            return;
        }

        // Check if user is authorized
        var authorizedIds = telegramBotSettings.Value.AuthorizedUserIds;
        if (authorizedIds.Length > 0 && !authorizedIds.Contains((long)userId))
        {
            logger.LogWarning("Unauthorized access attempt from user ID: {UserId}", userId);
            await botClient.SendMessage(
                (long)userId,
                "❌ You are not authorized to use this bot.",
                cancellationToken: cancellationToken);
            return;
        }

        var scope = serviceScopeFactory.CreateScope();
        var chatBotHandler = scope.ServiceProvider.GetRequiredService<IChatBotHandler>();
        await chatBotHandler.HandleAsync(botClient, update, cancellationToken);
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error: {Error}", exception);
        return Task.CompletedTask;
    }
}