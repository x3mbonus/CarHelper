// -----------------------------------------------------------------------
// <copyright file="WebAppExtensions.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CarNotesTelegramBot.Constants;
using CarNotesTelegramBot.DbContexts;
using CarNotesTelegramBot.Interfaces;
using CarNotesTelegramBot.Services;
using CarNotesTelegramBot.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarNotesTelegramBot;

public static class AppBuilderExtensions
{
    public static HostApplicationBuilder AddDomainServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(DomainConstants.DefaultConnection)));

        SetConfiguration(builder);

        builder.Services.AddTransient<IChatBotEntry, ChatBotEntry>();
        builder.Services.AddScoped<IChatBotHandler, ChatBotHandler>();
        builder.Services.AddScoped<IChatBotDefaultHandler, ChatBotDefaultHandler>();
        builder.Services.AddScoped<IChatBotStartHandler, ChatBotStartHandler>();

        return builder;
    }

    private static void SetConfiguration(IHostApplicationBuilder builder)
    {
        builder.ConfigureSettingsOptions<TelegramBotSettings>();
    }

    private static IHostApplicationBuilder ConfigureSettingsOptions<TSettings>(this IHostApplicationBuilder builder)
        where TSettings : class
    {
        builder.Services.Configure<TSettings>(builder.Configuration.GetSection(typeof(TSettings).Name));
        return builder;
    }
}