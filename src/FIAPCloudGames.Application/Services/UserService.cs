using FIAPCloudGames.Application.Adapters;
using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserReadDto> RegisterAsync(string name, string email, string password)
    {
        var user = new User(name, email, password);
        await _userRepository.AddAsync(user);
        return user.ToDto();
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.ToDto();
    }

    public async Task<UserReadDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user?.ToDto();
    }

    public async Task<UserReadDto?> UpdateAsync(Guid id, string name, string email, string password)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        user.UpdateName(name);
        user.UpdateEmail(email);
        user.UpdatePassword(password);
        await _userRepository.UpdateAsync(user);
        return user.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        await _userRepository.RemoveAsync(user);
        return true;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user != null && user.Password.Value == password)
        {
            return user;
        }

        return null;
    }
}
