// -----------------------------------------------------------------------
// <copyright file="IChatBotTransferDetailsHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Interfaces;

public interface IChatBotDefaultHandler
{
    Task HandleOtherMessagesAsync(
        ITelegramBotClient botClient,
        Message? message,
        CancellationToken cancellationToken);
}