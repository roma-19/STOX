using STOX.Service.DTOs.User;

namespace STOX.Service.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(Guid id);
    Task RegisterAsync(RegisterUserDto registerDto);
    Task<string> LoginAsync(LoginUserDto loginDto);
    Task UpdateAsync(UserDto userDto);
    Task DeleteAsync(Guid id);
}