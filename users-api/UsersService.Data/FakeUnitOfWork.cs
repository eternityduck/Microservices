using UsersService.Data.Interfaces;
using UsersService.Data.Interfaces.Repositories;

namespace UsersService.Data;
public sealed class FakeUnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public FakeUnitOfWork(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
