using CarNotesTelegramBot;
using CarNotesTelegramBot.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args)
    .AddDomainServices();
Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
var host = builder.Build();
var chatBotEntry = host.Services.GetRequiredService<IChatBotEntry>();
chatBotEntry.StartBot();
host.Run();