// -----------------------------------------------------------------------
// <copyright file="ChatBotImageHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CarNotesTelegramBot.DbContexts;
using CarNotesTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Services;

public class ChatBotFeatureHandler(AppDbContext  dbContext)
    : IChatBotFeatureHandler
{
    public async Task HandleFeatureCommandAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
    }

    private (string? CarId, string Error) TryParseCarId(string? messageText)
    {
        if (string.IsNullOrEmpty(messageText))
        {
            return (null, string.Empty);
        }

        return (null, "Car ID not found in URL");
    }
}