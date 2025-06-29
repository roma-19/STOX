using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using STOX.Data.Entities;
using STOX.Data.Enums;
using STOX.Repo.Interfaces;

using STOX.Service.DTOs.User;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(IRepository<User> repository, IMapper mapper, IConfiguration configuration, IUserRepository userRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return users.Select(u => _mapper.Map<UserDto>(u));
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetByKeysAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task RegisterAsync(RegisterUserDto registerDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }
        
        var totalUsers = await _repository.CountAsync();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = hashedPassword,
            ContactInfo = registerDto.ContactInfo,
            Role = totalUsers == 0 ? Role.Admin : Role.Client
        };

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginUserDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        
        var token = GenerateJwtToken(user);
        return token;
    }

    public async Task UpdateAsync(UserDto userDto)
    {
        var user = await _repository.GetByKeysAsync(userDto.Id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        _mapper.Map(userDto, user);
        _repository.Update(user);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByKeysAsync(id);
        if (user != null)
        {
            _repository.Delete(user);
            await _repository.SaveChangesAsync();
        }
    }
    
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}