using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarNotesTelegramBot.Entities;

/// <summary>
/// The transfer entity class.
/// </summary>
public class FeaturesConfig
{
    /// <summary>
    /// Gets or sets the value of the id.
    /// </summary>
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the value of the key.
    /// </summary>
    [Column("key")]
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the label.
    /// </summary>
    [Column("label")]
    public string Label { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the priority.
    /// </summary>
    [Column("priority")]
    public short? Priority { get; set; }

    /// <summary>
    /// Gets or sets the value of the sort.
    /// </summary>
    [Column("sort")]
    public short? Sort { get; set; }

    /// <summary>
    /// Gets or sets the value of the created at.
    /// </summary>
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the value of the updated at.
    /// </summary>
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}