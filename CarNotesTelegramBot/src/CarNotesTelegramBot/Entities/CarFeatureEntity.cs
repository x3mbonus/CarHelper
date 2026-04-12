using System.ComponentModel.DataAnnotations.Schema;

namespace CarNotesTelegramBot.Entities;

/// <summary>
/// The transfer entity class.
/// </summary>
public class CarFeatureEntity
{
    /// <summary>
    /// Gets or sets the value of the id.
    /// </summary>
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the value of the car id.
    /// </summary>
    [Column("car_id")]
    public string CarId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the feature id.
    /// </summary>
    [Column("feature_id")]
    public long FeatureId { get; set; }

    /// <summary>
    /// Gets or sets the value of the state.
    /// </summary>
    [Column("state")]
    public bool? State { get; set; }

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

    /// <summary>
    /// Gets or sets the value of the feature.
    /// </summary>
    public FeaturesConfig Feature { get; set; } = null!;
}