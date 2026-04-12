// -----------------------------------------------------------------------
// <copyright file="CarDataEntity.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarNotesTelegramBot.Entities;

/// <summary>
/// The car data entity class.
/// </summary>
public class CarDataEntity
{
    /// <summary>
    /// Gets or sets the value of the id.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the value of the car id.
    /// </summary>
    [Column("car_id")]
    public string CarId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the text.
    /// </summary>
    [Column("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the value of the color.
    /// </summary>
    [Column("color")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the value of the sort.
    /// </summary>
    [Column("sort")]
    public int? Sort { get; set; }

    /// <summary>
    /// Gets or sets the value of the confirmed.
    /// </summary>
    [Column("confirmed")]
    public bool? Confirmed { get; set; }

    /// <summary>
    /// Gets or sets the value of the features source.
    /// </summary>
    [Column("features_source")]
    public string? FeaturesSource { get; set; }

    /// <summary>
    /// Gets or sets the value of the make.
    /// </summary>
    [Column("make")]
    public string? Make { get; set; }

    /// <summary>
    /// Gets or sets the value of the model.
    /// </summary>
    [Column("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the value of the price.
    /// </summary>
    [Column("price")]
    public string? Price { get; set; }

    /// <summary>
    /// Gets or sets the value of the price eur.
    /// </summary>
    [Column("price_eur")]
    public string? PriceEur { get; set; }

    /// <summary>
    /// Gets or sets the value of the year.
    /// </summary>
    [Column("year")]
    public string? Year { get; set; }

    /// <summary>
    /// Gets or sets the value of the mileage.
    /// </summary>
    [Column("mileage")]
    public string? Mileage { get; set; }

    /// <summary>
    /// Gets or sets the value of the address.
    /// </summary>
    [Column("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the value of the interior.
    /// </summary>
    [Column("interior")]
    public string? Interior { get; set; }

    /// <summary>
    /// Gets or sets the value of the seat type.
    /// </summary>
    [Column("seat_type")]
    public string? SeatType { get; set; }

    /// <summary>
    /// Gets or sets the value of the climate.
    /// </summary>
    [Column("climate")]
    public string? Climate { get; set; }

    /// <summary>
    /// Gets or sets the value of the owners.
    /// </summary>
    [Column("owners")]
    public string? Owners { get; set; }

    /// <summary>
    /// Gets or sets the value of the tow hitch type.
    /// </summary>
    [Column("tow_hitch_type")]
    public string? TowHitchType { get; set; }

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