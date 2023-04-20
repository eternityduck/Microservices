using Microsoft.EntityFrameworkCore;
using Polly;
using UsersService.Data.Persistence;

namespace UsersService.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        var migrateDbPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(4, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 3));

        migrateDbPolicy.Execute(() =>
        {
            db.Database.Migrate();
        });

        return app;
    }
}
