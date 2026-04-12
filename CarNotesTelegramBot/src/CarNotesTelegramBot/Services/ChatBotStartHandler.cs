// -----------------------------------------------------------------------
// <copyright file="ChatBotImageHandler.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CarNotesTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarNotesTelegramBot.Services;

public class ChatBotStartHandler()
    : IChatBotStartHandler
{
    public async Task HandleStartCommandAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        await botClient.SendMessage(
            chatId,
            "Вишли сюди посилання на авто з mobile.de чи otomoto.pl",
            cancellationToken: cancellationToken);
    }
}