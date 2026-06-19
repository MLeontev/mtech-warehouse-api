using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarehouseApi.Infrastructure.Seed;

namespace WarehouseApi.Infrastructure;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();
        await DatabaseSeeder.Seed(dbContext);
    }
}