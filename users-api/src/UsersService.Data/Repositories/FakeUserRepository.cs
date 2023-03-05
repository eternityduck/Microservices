using UsersService.Data.Entities;
using UsersService.Data.Interfaces.Repositories;

namespace UsersService.Data.Repositories;
public sealed class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User
        {
            Id = 1,
            FirstName = "Bohdan",
            LastName = "Karashchuk",
            Email = "iamnotaduck@fakemail.com",
            PhoneNumber = "380682888888"
        },
        new User
        {
            Id = 2,
            FirstName = "Artem",
            LastName = "Dankov",
            Email = "asapforver@fakemail.com",
            PhoneNumber = "380682999999"
        },
        new User
        {
            Id = 3,
            FirstName = "Nikita",
            LastName = "Chernikov",
            Email = "nchernikov@fakemail.com",
            PhoneNumber = "380682777777",
            BirthDay = new DateOnly(2003, 02, 14)
        },
        new User
        {
            Id = 4,
            FirstName = "Ivan",
            LastName = "Kyrylov",
            Email = "malchik_tamagochi@fakemail.com",
            PhoneNumber = "380682000000"
        },
    };

    private readonly object _lockObject = new();

    private static int _usersCreatedCount;

    public FakeUserRepository()
    {
        _usersCreatedCount = _users.Count;
    }

    public Task<IReadOnlyCollection<User>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyCollection<User>>(_users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public void Add(User user)
    {
        lock (_lockObject)
        {
            _usersCreatedCount++;

            user.Id = _usersCreatedCount;

            _users.Add(user);
        }
    }

    public void Update(User user)
    {
        lock (_lockObject)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            _users[index] = user;
        }
    }

    public void Delete(User user)
    {
        lock (_lockObject) 
        {
            _users.Remove(user);
        }
    }

    public Task<bool> IsEmailTakenAsync(string email, int? ignoreId)
    {
        return Task.FromResult(_users.Any(u => u.Email == email && u.Id != ignoreId));
    }

    public Task<bool> IsPhoneNumberTakenAsync(string phoneNumber, int? ignoreId)
    {
        return Task.FromResult(_users.Any(u => u.PhoneNumber == phoneNumber && u.Id != ignoreId));
    }
}
