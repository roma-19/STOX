using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface ICartItemService
{
    Task<IEnumerable<CartItemDto>> GetAllAsync();
    Task<CartItemDto> GetByIdAsync(Guid id);
    Task CreateAsync(CartItemDto cartItemDto);
    Task UpdateAsync(CartItemDto cartItemDto);
    Task DeleteAsync(Guid id);
}