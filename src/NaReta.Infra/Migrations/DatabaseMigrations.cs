using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NaReta.Infra.DataAccess;

namespace NaReta.Infra.Migrations;

public static class DatabaseMigrations
{
    public static async Task  MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<NaRetaDBContext>();
        await dbContext.Database.MigrateAsync();
    }
}
