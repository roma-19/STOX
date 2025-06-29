using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartDto>> GetAllAsync();
    Task<CartDto> GetByIdAsync(Guid id);
    Task CreateAsync(CartDto cartDto);
    Task UpdateAsync(CartDto cartDto);
    Task DeleteAsync(Guid id);
}