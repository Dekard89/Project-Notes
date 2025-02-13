using Microsoft.Extensions.Caching.Distributed;
using Note.DAL;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace NoteApi.Extensions;

public static class  ServiceCollectionsextensions
{
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "redis:6379,abortConnect=false";
            options.InstanceName = "noteapi";
        });
        builder.Services.AddDbContext<NoteContext>();
        return builder;
    }
}