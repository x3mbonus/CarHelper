// -----------------------------------------------------------------------
// <copyright file="IChatBotHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Interfaces;

/// <summary>
/// The chat bot handler interface.
/// </summary>
public interface IChatBotHandler
{
    /// <summary>
    /// Handles the update using the specified bot client.
    /// </summary>
    /// <param name="botClient">The bot client.</param>
    /// <param name="update">The update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}