using Microsoft.EntityFrameworkCore;
using UsersService.Data.Persistence;

namespace UsersService.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        db.Database.Migrate();

        return app;
    }
}
