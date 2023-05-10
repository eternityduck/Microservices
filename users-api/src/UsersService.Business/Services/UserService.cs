using AutoMapper;
using UsersService.Business.Dtos;
using UsersService.Business.Exceptions;
using UsersService.Business.Interfaces.Communication;
using UsersService.Business.Interfaces.Services;
using UsersService.Data.Entities;
using UsersService.Data.Interfaces;

namespace UsersService.Business.Services;
public sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IProductsHttpClient _ordersServiceClient;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IProductsHttpClient ordersServiceClient)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordersServiceClient = ordersServiceClient;
    }

    public async Task<IReadOnlyCollection<UserDto>> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        return _mapper.Map<IReadOnlyCollection<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId)
            ?? throw new EntityNotFoundException($"User with id {userId} was not found");

        var getProductsTask = _ordersServiceClient.GetUserOrdersAsync(user.Id);

        var result = _mapper.Map<UserDto>(user);
        result.Products = await getProductsTask;

        return result;
    }

    public async Task<UserDto> AddAsync(UserDto user)
    {
        await ValidateUserAsync(user);

        var userEntity = _mapper.Map<User>(user);
        _unitOfWork.UserRepository.Add(userEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> UpdateAsync(UserDto user)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(user.Id)
            ?? throw new EntityNotFoundException($"User with id {user.Id} was not found");

        await ValidateUserAsync(user);

        existingUser = _mapper.Map(user, existingUser);

        _unitOfWork.UserRepository.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(existingUser);
    }

    private async Task ValidateUserAsync(UserDto user)
    {
        if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.PhoneNumber))
        {
            throw new UnprocessableEntityException("Either phone number or email must not be empty");
        }

        if (!string.IsNullOrEmpty(user.Email)
            && await _unitOfWork.UserRepository.IsEmailTakenAsync(user.Email, user.Id))
        {
            throw new UnprocessableEntityException($"Email {user.Email} is already taken");
        }

        if (!string.IsNullOrEmpty(user.PhoneNumber)
            && await _unitOfWork.UserRepository.IsPhoneNumberTakenAsync(user.PhoneNumber, user.Id))
        {
            throw new UnprocessableEntityException($"Phone number {user.PhoneNumber} is already taken");
        }

        var currentTime = DateTime.UtcNow;
        const int minAge = 14;
        if (user.BirthDay is not null 
            && (currentTime.Year - user.BirthDay.Value.Year < minAge  
                || (currentTime.Year - user.BirthDay.Value.Year == minAge && user.BirthDay.Value.Month > currentTime.Month)
                || (currentTime.Year - user.BirthDay.Value.Year == minAge && user.BirthDay.Value.Month == currentTime.Month && user.BirthDay.Value.Day >= currentTime.Day)))
        {
            throw new UnprocessableEntityException($"Minimal age allowed is {minAge}");
        }
    }

    public async Task DeleteByIdAsync(int userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId)
            ?? throw new EntityNotFoundException($"User with id {userId} was not found");

        _unitOfWork.UserRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
