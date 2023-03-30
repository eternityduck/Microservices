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
}
