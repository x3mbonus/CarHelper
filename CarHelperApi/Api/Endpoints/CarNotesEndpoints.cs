// -----------------------------------------------------------------------
// <copyright file="CarNotesEndpoints.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public static class CarNotesEndpoints
{
    public static void AddCarNotesEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/CarNotes", () =>
            {
                return Results.Ok();
            })
            .WithName("CarNotes");

    }
}