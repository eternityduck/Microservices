using UsersService.Data.Entities;

namespace UsersService.Data.Interfaces.Repositories;
public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync();

    Task<User?> GetByIdAsync(int id);

    Task<bool> IsEmailTakenAsync(string email, int? ignoreId = null);

    Task<bool> IsPhoneNumberTakenAsync(string phoneNumber, int? ignoreId = null);

    void Add(User user);

    void Update(User user);

    void Delete(User user);
}
