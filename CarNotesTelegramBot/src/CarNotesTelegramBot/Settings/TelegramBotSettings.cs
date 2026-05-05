namespace CarNotesTelegramBot.Settings;

/// <summary>
/// The telegram bot settings class.
/// </summary>
public class TelegramBotSettings
{
    /// <summary>
    /// Gets or sets the value of the token.
    /// </summary>
    public string CarNotesToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of authorized user IDs.
    /// </summary>
    public long[] AuthorizedUserIds { get; set; } = [];
}