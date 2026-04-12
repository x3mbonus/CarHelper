// -----------------------------------------------------------------------
// <copyright file="HostApplicationBuilderExtension.cs" company="iLoy Solutions SA">
// Copyright (c) iLoy Solutions SA. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Api;

public static class HostApplicationBuilderExtension
{
    private const string DefaultConnection = "DefaultConnection";

    public static IHostApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(DefaultConnection);
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        return builder;
    }
}