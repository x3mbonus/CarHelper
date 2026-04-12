// -----------------------------------------------------------------------
// <copyright file="IChatBotService.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CarNotesTelegramBot.Interfaces;

/// <summary>
/// The chat bot entry interface.
/// </summary>
public interface IChatBotEntry
{
    /// <summary>
    /// Starts the bot.
    /// </summary>
    void  StartBot();
}