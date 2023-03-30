using UsersService.Data.Interfaces;
using UsersService.Data.Interfaces.Repositories;
using UsersService.Data.Persistence;

namespace UsersService.Data;
public sealed class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    private readonly UsersDbContext _dbContext;

    public UnitOfWork(UsersDbContext dbContext, 
        IUserRepository userRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
    }

    public void Rollback()
    {
        _dbContext.ChangeTracker.Clear();
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
