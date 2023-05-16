using Microsoft.EntityFrameworkCore;
using UsersService.Data.Entities;

namespace UsersService.Data.Persistence;
public class UsersDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public UsersDbContext(DbContextOptions<UsersDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                BirthDay = new DateOnly(2000, 1, 1),
                Email = "admin@fakemail.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "111111111111"
            });
    }
}
