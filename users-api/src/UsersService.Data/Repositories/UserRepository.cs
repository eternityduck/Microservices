using Microsoft.EntityFrameworkCore;
using UsersService.Data.Entities;
using UsersService.Data.Interfaces.Repositories;
using UsersService.Data.Persistence;

namespace UsersService.Data.Repositories;
public sealed class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public Task<bool> IsEmailTakenAsync(string email, int? ignoreId)
    {
        return _dbContext.Users.AnyAsync(u => u.Email == email && u.Id != ignoreId);
    }

    public Task<bool> IsPhoneNumberTakenAsync(string phoneNumber, int? ignoreId)
    {
        return _dbContext.Users.AnyAsync(u => u.PhoneNumber == phoneNumber && u.Id != ignoreId);
    }
}
