// -----------------------------------------------------------------------
// <copyright file="ChatBotMessageHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CarNotesTelegramBot.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Services;

public class ChatBotHandler(
    ILogger<ChatBotHandler> logger,
    IChatBotStartHandler chatBotStartHandler,
    IChatBotDefaultHandler chatBotDefaultHandler) : IChatBotHandler
{
    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        switch (message?.Text)
        {
            case "/start":
                await chatBotStartHandler.HandleStartCommandAsync(botClient, message, cancellationToken);
                break;
            case { } s when s.StartsWith("/feature", StringComparison.OrdinalIgnoreCase):
                // await chatBotStartHandler.HandleStartCommandAsync(botClient, message, cancellationToken);
                break;
            default:
                await chatBotDefaultHandler.HandleOtherMessagesAsync(botClient, message, cancellationToken);
                break;
        }
    }
}